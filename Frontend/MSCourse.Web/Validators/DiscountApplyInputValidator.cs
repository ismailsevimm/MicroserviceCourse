using FluentValidation;
using MSCourse.Web.Models.DiscountModels;

namespace MSCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Discount code cannot be empty!");
        }
    }
}
