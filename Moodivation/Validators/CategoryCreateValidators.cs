using FluentValidation;
using Moodivation.Entities.Dtos;

namespace Moodivation.Validators
{
    public class CategoryCreateValidators : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidators()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Enter category name");
        }
    }
}
