
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.OrganizationId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
