using GestaoPedidos.Data.DataBase.GestaoPedidos.Data.DataBase;
using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Domain.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GestaoPedidosContext _context;

        public ProdutoRepository(GestaoPedidosContext context)
        {
            _context = context;
        }

        public async Task Atualizar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> Inserir(Produto produto)
        {
            _context.Produto.Add(produto);

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<ProdutoResponseDto?> PesquisarDtoPorId(int produtoId)
        {
            return await _context.Produto
                .AsNoTracking()
                .Where(w => w.Id == produtoId)
                .Select(s => new ProdutoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    EstoqueDisponivel = s.EstoqueDisponivel,
                    Ativo = s.Ativo,
                    DataCriacao = s.DataCriacao,
                    DataAtualizacao = s.DataAtualizacao
                }).FirstOrDefaultAsync();
        }

        public async Task<Produto?> PesquisarProdutoPorId(int produtoId)
        {
            return await _context.Produto
                .Where(x => x.Id == produtoId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Produto>> PesquisarProdutosPorIds(List<int> produtosIds)
        {
            if (produtosIds == null || !produtosIds.Any())
                return new List<Produto>();

            return await _context.Produto
                .Where(x => produtosIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<List<ProdutoResponseDto>> PesquisarTodos()
        {
            return await _context.Produto
                .AsNoTracking()
                .Select(s => new ProdutoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    EstoqueDisponivel = s.EstoqueDisponivel,
                    Ativo = s.Ativo,
                    DataCriacao = s.DataCriacao,
                    DataAtualizacao = s.DataAtualizacao
                }).ToListAsync();
        }
    }
}
