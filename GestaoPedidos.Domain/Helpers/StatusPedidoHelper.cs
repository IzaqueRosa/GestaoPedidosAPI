using GestaoPedidos.Data.Models;

namespace GestaoPedidos.Domain.Helpers
{
    public  class StatusPedidoHelper
    {
        public static bool PodeAlterar(StatusPedido atual, StatusPedido novo)
        {
            return (atual, novo) switch
            {
                (StatusPedido.Criado, StatusPedido.Pago) => true,
                (StatusPedido.Pago, StatusPedido.Enviado) => true,
                (StatusPedido.Criado, StatusPedido.Cancelado) => true,
                _ => false
            };
        }
    }
}
