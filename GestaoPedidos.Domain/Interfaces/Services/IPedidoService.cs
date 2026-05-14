using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<object?> AlterarStatus(string id, string status);
        Task<object?> Inserir(PedidoRequestDto pedidoRequestDto);
        Task<object?> PesquisarPorId(string id);
        Task<object?> PesquisarTodos();
    }
}
