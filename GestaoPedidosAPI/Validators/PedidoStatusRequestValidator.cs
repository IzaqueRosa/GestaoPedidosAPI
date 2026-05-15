using FluentValidation;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidosAPI.Validators
{
    public class PedidoStatusRequestValidator : AbstractValidator<PedidoStatusRequestDto>
    {
        public PedidoStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status do pedido inválido.");

            RuleFor(x => x.MotivoAlteracao)
                .MaximumLength(2000)
                .WithMessage(
                    "Motivo deve possuir no máximo 2000 caracteres.");
        }
    }
}
