using FluentValidation;
using SMG4._0.Models.Request;

namespace SMG4._0.Helpers.Validators
{
    public class AuthenticationRequestModelValidator : AbstractValidator<AuthenticationRequestModel>
    {
        public AuthenticationRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().Length(10, 50);
        }
    }
}
