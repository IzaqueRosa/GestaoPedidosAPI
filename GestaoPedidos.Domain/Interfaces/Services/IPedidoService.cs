using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task AlterarStatus(string id, PedidoStatusRequestDto pedidoStatusRequestDto);
        Task<PedidoResponseDto> Inserir(PedidoRequestDto pedidoRequestDto);
        Task<PedidoResponseDto> PesquisarPorId(string id);
        Task<List<PedidoResponseDto>> PesquisarTodos();
    }
}
