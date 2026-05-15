using GestaoPedidos.Data.DataBase.GestaoPedidos.Data.DataBase;
using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Exceptions;
using GestaoPedidos.Domain.Helpers;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;

namespace GestaoPedidos.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly GestaoPedidosContext _context;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository,
            GestaoPedidosContext context)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _context = context;
        }

        public async Task AlterarStatus(string id, PedidoStatusRequestDto pedidoStatusRequestDto)
        {
            if (!int.TryParse(id, out int pedidoId))
            {
                throw new BusinessException("Pedido inválido.");
            }

            var pedido = await _pedidoRepository
                .PesquisarPedidoCompletoPorId(pedidoId);

            if (pedido == null)
            {
                throw new BusinessException("Pedido não encontrado.");
            }

            var statusAtual = pedido.Status;
            var novoStatus = pedidoStatusRequestDto.Status;

            if (statusAtual == novoStatus)
            {
                throw new BusinessException(
                    $"O pedido já está com status {statusAtual}.");
            }

            if (!StatusPedidoHelper.PodeAlterar(statusAtual, novoStatus))
            {
                throw new BusinessException(
                    $"Transição inválida de {statusAtual} para {novoStatus}.");
            }

            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                if (novoStatus == StatusPedido.Cancelado)
                {
                    foreach (var item in pedido.Itens)
                    {
                        item.Produto.EstoqueDisponivel += item.Quantidade;
                    }
                }

                pedido.Status = novoStatus;

                pedido.Historicos.Add(new PedidoHistorico
                {
                    StatusAnterior = statusAtual,
                    NovoStatus = novoStatus,
                    DataHoraAlteracao = DateTimeOffset.UtcNow,
                    Motivo = pedidoStatusRequestDto.MotivoAlteracao
                });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PedidoResponseDto> Inserir(PedidoRequestDto pedidoRequestDto)
        {
            if (pedidoRequestDto == null)
                throw new BusinessException("Pedido inválido.");

            var cliente = await _clienteRepository
                .PesquisarClientePorId(pedidoRequestDto.ClienteId);

            if (cliente == null)
                throw new BusinessException("Cliente não encontrado.");

            if (!cliente.Ativo)
                throw new BusinessException("Cliente inativo.");

            if (pedidoRequestDto.Itens == null
                || !pedidoRequestDto.Itens.Any())
            {
                throw new BusinessException(
                    "O pedido deve possuir ao menos um item.");
            }

            if (pedidoRequestDto.Itens.Any(x => x.Quantidade <= 0))
            {
                throw new BusinessException(
                    "Todos os itens devem possuir quantidade maior que zero.");
            }

            var itensAgrupados = pedidoRequestDto.Itens
                .GroupBy(x => x.ProdutoId)
                .Select(x => new
                {
                    ProdutoId = x.Key,
                    Quantidade = x.Sum(y => y.Quantidade)
                })
                .ToList();

            var produtosIds = itensAgrupados
                .Select(x => x.ProdutoId)
                .Distinct()
                .ToList();

            var produtos = await _produtoRepository
                .PesquisarProdutosPorIds(produtosIds);

            var produtosDictionary = produtos
                .ToDictionary(x => x.Id);

            foreach (var itemAgrupado in itensAgrupados)
            {
                if (!produtosDictionary.TryGetValue(
                    itemAgrupado.ProdutoId,
                    out var produto))
                {
                    throw new BusinessException(
                        $"Produto {itemAgrupado.ProdutoId} não encontrado.");
                }

                if (!produto.Ativo)
                {
                    throw new BusinessException(
                        $"Produto {itemAgrupado.ProdutoId} está inativo.");
                }

                if (produto.EstoqueDisponivel < itemAgrupado.Quantidade)
                {
                    throw new BusinessException(
                        $"Estoque insuficiente para o produto {itemAgrupado.ProdutoId}.");
                }
            }

            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var pedido = new Pedido
                {
                    ClienteId = pedidoRequestDto.ClienteId,
                    DataCriacao = DateTimeOffset.UtcNow,
                    Status = StatusPedido.Criado,
                    ValorTotal = 0,
                    Itens = new List<PedidoItem>(),
                    Historicos = new List<PedidoHistorico>()
                };

                decimal valorTotalPedido = 0;

                foreach (var itemDto in pedidoRequestDto.Itens)
                {
                    if (!produtosDictionary.TryGetValue(
                        itemDto.ProdutoId,
                        out var produto))
                    {
                        throw new BusinessException(
                            $"Produto {itemDto.ProdutoId} não encontrado.");
                    }

                    decimal precoUnitario = produto.Preco;

                    decimal valorTotalItem =
                        precoUnitario * itemDto.Quantidade;

                    produto.EstoqueDisponivel -= itemDto.Quantidade;

                    pedido.Itens.Add(new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Quantidade = itemDto.Quantidade,
                        PrecoUnitario = precoUnitario,
                        ValorTotalItem = valorTotalItem
                    });

                    valorTotalPedido += valorTotalItem;
                }

                pedido.ValorTotal = valorTotalPedido;

                pedido.Historicos.Add(new PedidoHistorico
                {
                    StatusAnterior = null,
                    NovoStatus = StatusPedido.Criado,
                    DataHoraAlteracao = DateTimeOffset.UtcNow,
                    Motivo = "Pedido criado."
                });

                await _pedidoRepository.Inserir(pedido);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new PedidoResponseDto
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    Status = pedido.Status,
                    DataCriacao = pedido.DataCriacao,
                    ValorTotal = pedido.ValorTotal,

                    Itens = pedido.Itens
                        .Select(x => new PedidoItemResponseDto
                        {
                            ProdutoId = x.ProdutoId,
                            Quantidade = x.Quantidade,
                            PrecoUnitario = x.PrecoUnitario,
                            ValorTotalItem = x.ValorTotalItem
                        })
                        .ToList()
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PedidoResponseDto> PesquisarPorId(string id)
        {
            if (!int.TryParse(id, out int pedidoId))
            {
                throw new BusinessException("Pedido inválido.");
            }

            var pedido = await _pedidoRepository.PesquisarPedidoPorId(pedidoId);

            if (pedido == null)
            {
                throw new BusinessException("Pedido não encontrado.");
            }

            return pedido;
        }

        public async Task<List<PedidoResponseDto>> PesquisarTodos()
        {
            return await _pedidoRepository.PesquisarTodos();
        }
    }
}
