
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task Inativar(string id);
        Task<ClienteResponseDto> Inserir(ClienteRequestDto clienteRequestDto);
        Task<ClienteResponseDto?> PesquisarDtoPorId(string id);
        Task<List<ClienteResponseDto>> PesquisarTodos();
    }
}
