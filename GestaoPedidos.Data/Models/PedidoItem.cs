using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("PEDIDO_ITEM")]
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotalItem { get; set; }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
    }
}
