using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("PEDIDO_HISTORICO")]
    public class PedidoHistorico
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public StatusPedido? StatusAnterior { get; set; }
        public StatusPedido NovoStatus { get; set; }
        public DateTimeOffset DataHoraAlteracao { get; set; }
        public string Motivo { get; set; }
    }
}
