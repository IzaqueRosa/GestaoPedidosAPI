namespace GestaoPedidos.Data.Models.Dtos
{
    public class ClienteRequestDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public bool Ativo {  get; set; }
    }
}
