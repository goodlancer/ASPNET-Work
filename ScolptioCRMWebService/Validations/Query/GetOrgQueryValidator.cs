
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class GetOrgQueryValidator : AbstractValidator<GetOrgQuery>
    {
        public GetOrgQueryValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty();
        }
    }
}
