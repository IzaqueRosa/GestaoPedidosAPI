using FluentValidation;
using GestaoPedidos.Data.Models.Dtos;

namespace GestaoPedidosAPI.Validators
{
    public class ClienteRequestValidator : AbstractValidator<ClienteRequestDto>
    {
        public ClienteRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.Documento)
                .NotEmpty()
                .WithMessage("Documento é obrigatório.");
        }
    }
}
