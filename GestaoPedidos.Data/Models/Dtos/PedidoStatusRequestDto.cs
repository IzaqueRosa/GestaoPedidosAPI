namespace GestaoPedidos.Data.Models.Dtos
{
    public class PedidoStatusRequestDto
    {
        public StatusPedido Status { get; set; }
        public string MotivoAlteracao { get; set; }
    }
}
