namespace GestaoPedidos.Data.Models.Dtos
{
    public class ProdutoRequestDto
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int EstoqueDisponivel { get; set; }

        public bool Ativo { get; set; }
    }
}
