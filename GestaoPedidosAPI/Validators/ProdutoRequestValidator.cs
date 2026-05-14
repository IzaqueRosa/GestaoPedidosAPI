using FluentValidation;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidosAPI.Validators
{
    public class ProdutoRequestValidator : AbstractValidator<ProdutoRequestDto>
    {
        public ProdutoRequestValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty()
                .WithMessage("Nome do produto é obrigatório.");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero.");

            RuleFor(x => x.EstoqueDisponivel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Estoque não pode ser negativo.");
        }
    }
}
