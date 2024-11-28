

using AmazonECommerce.Application.DTOs.Products;
using FluentValidation;

namespace AmazonECommerce.Application.Validators.ProductValidators;

public class ProductUpdateValidator : AbstractValidator<ProductUpdate>
{
    public ProductUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required");
    }
}