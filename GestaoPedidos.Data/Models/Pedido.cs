using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("PEDIDO")]
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<PedidoItem> Itens { get; set; }
            = new List<PedidoItem>();
        public ICollection<PedidoHistorico> Historicos { get; set; }
            = new List<PedidoHistorico>();
    }

    public enum StatusPedido
    {
        Criado = 1,
        Pago = 2,
        Enviado = 3,
        Cancelado = 4
    }
}
