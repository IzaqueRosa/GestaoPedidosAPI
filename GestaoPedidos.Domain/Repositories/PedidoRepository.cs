using GestaoPedidos.Data.DataBase.GestaoPedidos.Data.DataBase;
using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Domain.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly GestaoPedidosContext _context;

        public PedidoRepository(GestaoPedidosContext context)
        {
            _context = context;
        }

        public async Task Inserir(Pedido pedido)
        {
            await _context.Pedido.AddAsync(pedido);
        }
        
        public async Task<Pedido?> PesquisarPedidoCompletoPorId(int pedidoId)
        {
            return await _context.Pedido
                .Include(x => x.Itens)
                    .ThenInclude(x => x.Produto)
                .Include(x => x.Historicos)
                .FirstOrDefaultAsync(x => x.Id == pedidoId);
        }

        public async Task<PedidoResponseDto?> PesquisarPedidoPorId(int pedidoId)
        {
            return await _context.Pedido
                .AsNoTracking()
                .Where(x => x.Id == pedidoId)
                .Select(x => new PedidoResponseDto
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    DataCriacao = x.DataCriacao,
                    Status = x.Status,
                    ValorTotal = x.ValorTotal,

                    Itens = x.Itens
                        .Select(item => new PedidoItemResponseDto
                        {
                            ProdutoId = item.ProdutoId,
                            Quantidade = item.Quantidade,
                            PrecoUnitario = item.PrecoUnitario,
                            ValorTotalItem = item.ValorTotalItem
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<PedidoResponseDto>> PesquisarTodos()
        {
            return await _context.Pedido
                .AsNoTracking()
                .Select(x => new PedidoResponseDto
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    DataCriacao = x.DataCriacao,
                    Status = x.Status,
                    ValorTotal = x.ValorTotal,

                    Itens = x.Itens
                        .Select(item => new PedidoItemResponseDto
                        {
                            ProdutoId = item.ProdutoId,
                            Quantidade = item.Quantidade,
                            PrecoUnitario = item.PrecoUnitario,
                            ValorTotalItem = item.ValorTotalItem
                        })
                        .ToList()
                })
                .ToListAsync();
        }
    }
}
