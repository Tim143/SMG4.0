using FluentValidation;
using SMG4._0.Models.Request;

namespace SMG4._0.Helpers.Validators
{
    public class ActivateEmployeeRequestModelValidator : AbstractValidator<ActivateEmployeeRequestModel>
    {
        public ActivateEmployeeRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.ActivationCode).NotEmpty().Length(10, 50);
            RuleFor(x => x.Password).NotEmpty().Length(10, 50);
            RuleFor(x => x.PasswordRepeat).NotEmpty().Length(10, 50).Equal(x=> x.Password);
        }
    }
}
