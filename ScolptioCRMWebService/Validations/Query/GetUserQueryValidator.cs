
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
