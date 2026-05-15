using FluentValidation;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidosAPI.Validators
{
    public class PedidoRequestValidator
    : AbstractValidator<PedidoRequestDto>
    {
        public PedidoRequestValidator()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0)
                .WithMessage("Cliente inválido.");

            RuleFor(x => x.Itens)
                .NotNull()
                .Must(x => x.Any())
                .WithMessage("O pedido deve possuir ao menos um item.");

            RuleForEach(x => x.Itens)
                .SetValidator(new PedidoItemRequestValidator());
        }
    }
}
