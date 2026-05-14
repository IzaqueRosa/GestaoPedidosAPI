using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Exceptions;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;
using GestaoPedidos.Domain.Repositories;

namespace GestaoPedidos.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Atualizar(string id, ProdutoRequestDto produtoRequestDto)
        {
            var produto = await _produtoRepository.PesquisarProdutoPorId(int.Parse(id));

            if (produto == null)
                throw new BusinessException("Produto não encontrado.");

            produto.Nome = produtoRequestDto.Nome;
            produto.Descricao = produtoRequestDto.Descricao;
            produto.Preco = produtoRequestDto.Preco;
            produto.EstoqueDisponivel = produtoRequestDto.EstoqueDisponivel;
            produto.Ativo = produtoRequestDto.Ativo;
            produto.DataAtualizacao = DateTime.UtcNow;

            await _produtoRepository.Atualizar();
        }

        public async Task<ProdutoResponseDto> Inserir(ProdutoRequestDto produtoRequestDto)
        {
            var produto = new Produto
            {
                Nome = produtoRequestDto.Nome,
                Descricao = produtoRequestDto.Descricao,
                Preco = produtoRequestDto.Preco,
                EstoqueDisponivel = produtoRequestDto.EstoqueDisponivel,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            await _produtoRepository.Inserir(produto);

            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                EstoqueDisponivel= produto.EstoqueDisponivel,
                Ativo = produto.Ativo,
                DataCriacao = produto.DataCriacao,
                DataAtualizacao = produto.DataAtualizacao
            };
        }

        public async Task<List<ProdutoResponseDto>> PesquisarTodos()
        {
            return await _produtoRepository.PesquisarTodos();
        }

        public async Task<ProdutoResponseDto> PesquisarDtoPorId(string id)
        {
            var produtoResponseDto = await _produtoRepository.PesquisarDtoPorId(int.Parse(id));

            if (produtoResponseDto == null)
                throw new BusinessException("Produto não encontrado.");

            return produtoResponseDto;
        }

        public async Task Inativar(string id)
        {
            var produto = await _produtoRepository.PesquisarProdutoPorId(int.Parse(id));

            if (produto == null)
                throw new BusinessException("Produto não encontrado.");

            if (!produto.Ativo)
                throw new BusinessException("Produto já está inativo.");

            produto.Ativo = false;
            produto.DataAtualizacao = DateTime.UtcNow;

            await _produtoRepository.Atualizar();
        }
    }
}
