
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class CreateNewOrgCommandValidator : AbstractValidator<CreateNewOrgCommand>
    {
        public CreateNewOrgCommandValidator()
        {
            RuleFor(x => x.OrgName).NotEmpty();
        }
    }
}
