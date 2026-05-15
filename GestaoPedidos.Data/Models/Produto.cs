using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("PRODUTO")]
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int EstoqueDisponivel { get; set; }
        public bool Ativo { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataAtualizacao { get; set; }
        public ICollection<PedidoItem> PedidoItens { get; set; }
            = new List<PedidoItem>();
    }
}
