using FluentValidation;
using MSCourse.Web.Models.CatalogModels;

namespace MSCourse.Web.Validators
{
    public class CourseUpdateInputValidator:AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Course cannot be empty!");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description cannot be empty!");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Category cannot be empty!");
            RuleFor(c => c.Price).NotEmpty().WithMessage("Price cannot be empty!").ScalePrecision(2, 6).WithMessage("Incorrect price cannot be entered!");
            RuleFor(c => c.Feature.Duration).InclusiveBetween(1, 100).WithMessage("Duration cannot be empty!");
        }
    }
}
