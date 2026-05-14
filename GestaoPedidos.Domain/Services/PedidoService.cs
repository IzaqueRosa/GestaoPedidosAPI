using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Services;

namespace GestaoPedidos.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public Task<object?> AlterarStatus(string id, string status)
        {
            throw new NotImplementedException();
        }

        public Task<object?> Inserir(PedidoRequestDto pedidoRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<object?> PesquisarPorId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<object?> PesquisarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
