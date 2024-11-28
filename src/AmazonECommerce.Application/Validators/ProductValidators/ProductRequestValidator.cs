

using AmazonECommerce.Application.DTOs.Products;
using FluentValidation;

namespace AmazonECommerce.Application.Validators.ProductValidators;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required");
    }
}