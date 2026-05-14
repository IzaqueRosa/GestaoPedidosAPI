using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task Atualizar();
        Task<Produto> Inserir(Produto produto);
        Task<ProdutoResponseDto?> PesquisarDtoPorId(int id);
        Task<Produto> PesquisarProdutoPorId(int id);
        Task<List<ProdutoResponseDto>> PesquisarTodos();
    }
}
