namespace GestaoPedidos.Data.Models.Dtos
{
    public class PedidoRequestDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Status { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
