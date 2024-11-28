using AmazonECommerce.Application.DTOs.Categories;
using FluentValidation;

namespace AmazonECommerce.Application.Validators.CategoryValidators;

public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category Name cannot be empty");
    }
}