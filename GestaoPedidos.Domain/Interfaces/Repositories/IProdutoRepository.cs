using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task Atualizar();
        Task<Produto> Inserir(Produto produto);
        Task<ProdutoResponseDto?> PesquisarDtoPorId(int produtoId);
        Task<Produto?> PesquisarProdutoPorId(int produtoId);
        Task<List<Produto>> PesquisarProdutosPorIds(List<int> produtosIds);
        Task<List<ProdutoResponseDto>> PesquisarTodos();
    }
}
