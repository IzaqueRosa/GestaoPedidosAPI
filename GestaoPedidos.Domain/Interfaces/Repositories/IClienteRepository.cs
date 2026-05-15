using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<bool> ExisteEmailAtivo(string email);
        Task<bool> ExisteDocumentoAtivo(string documento);
        Task<Cliente> Inserir(Cliente cliente);
        Task<List<ClienteResponseDto>> PesquisarTodos();
        Task<ClienteResponseDto?> PesquisarDtoPorId(int clienteId);
        Task<Cliente?> PesquisarClientePorId(int clienteId);
        Task Atualizar();
    }
}
