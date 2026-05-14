using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("PEDIDO")]
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTimeOffset DataCriacao { get; set; }

        public string Status { get; set; }

        public decimal ValorTotal { get; set; }

    }
}
