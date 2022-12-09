
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class ExchangeTokenCommandValidator : AbstractValidator<ExchangeTokenCommand>
    {
        public ExchangeTokenCommandValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty();
        }
    }
}
