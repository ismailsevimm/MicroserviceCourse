using FluentValidation;
using MSCourse.Web.Models;

namespace MSCourse.Web.Validators
{
    public class SigninInputValidator:AbstractValidator<SigninInput>
    {
        public SigninInputValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}
