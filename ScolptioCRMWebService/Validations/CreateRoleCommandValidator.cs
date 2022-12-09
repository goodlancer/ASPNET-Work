
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.OrgId).NotEmpty();
            RuleFor(x => x.Permissions).NotEmpty();
        }
    }
}
