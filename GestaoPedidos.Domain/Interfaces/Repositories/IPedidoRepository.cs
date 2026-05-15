using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task Inserir(Pedido pedido);
        Task<Pedido?> PesquisarPedidoCompletoPorId(int pedidoId);
        Task<PedidoResponseDto?> PesquisarPedidoPorId(int pedidoId);
        Task<List<PedidoResponseDto>> PesquisarTodos();
    }
}
