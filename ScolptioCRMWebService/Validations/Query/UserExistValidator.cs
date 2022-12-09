
using Commands;

using FluentValidation;

namespace ScolptioCRMWebService.Validations
{
    public class UserExistQueryValidator : AbstractValidator<UserExistQuery>
    {
        public UserExistQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
