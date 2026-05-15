namespace GestaoPedidos.Data.Models.Dtos
{
    public class PedidoRequestDto
    {
        public int ClienteId { get; set; }
        public List<PedidoItemRequestDto> Itens { get; set; }
    }

    public class PedidoItemRequestDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
