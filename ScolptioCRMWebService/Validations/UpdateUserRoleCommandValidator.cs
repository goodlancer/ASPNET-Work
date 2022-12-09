
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty();
            RuleFor(x => x.Roles).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
