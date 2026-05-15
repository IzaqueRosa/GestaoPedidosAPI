using FluentValidation;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidosAPI.Validators
{
    public class PedidoItemRequestValidator
    : AbstractValidator<PedidoItemRequestDto>
    {
        public PedidoItemRequestValidator()
        {
            RuleFor(x => x.ProdutoId)
                .GreaterThan(0)
                .WithMessage("Produto inválido.");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage("Quantidade deve ser maior que zero.");
        }
    }
}
