namespace GestaoPedidos.Data.Models.Dtos
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemResponseDto> Itens { get; set; }
    }

    public class PedidoItemResponseDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotalItem { get; set; }
    }
}
