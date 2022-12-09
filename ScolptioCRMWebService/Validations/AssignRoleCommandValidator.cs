
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty();
            RuleFor(x => x.RoleIds).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
