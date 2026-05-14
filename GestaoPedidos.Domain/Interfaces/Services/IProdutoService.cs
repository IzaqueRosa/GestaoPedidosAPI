using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Task Inativar(string id);
        Task Atualizar(string id, ProdutoRequestDto produtoRequestDto);
        Task<ProdutoResponseDto> Inserir(ProdutoRequestDto produtoRequestDto);
        Task<ProdutoResponseDto> PesquisarDtoPorId(string id);
        Task<List<ProdutoResponseDto>> PesquisarTodos();
    }
}
