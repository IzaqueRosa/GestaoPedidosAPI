using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Data.Models
{
    [Table("CLIENTE")]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public bool Ativo { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataAtualizacao { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
            = new List<Pedido>();
    }
}
