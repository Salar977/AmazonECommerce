using AmazonECommerce.Application.DTOs.Categories;
using FluentValidation;

namespace AmazonECommerce.Application.Validators.CategoryValidators;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdate>
{
    public CategoryUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category Name cannot be empty");
    }
}