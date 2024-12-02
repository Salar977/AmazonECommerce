using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Identity;
using AmazonECommerce.Application.Interfaces.Validation;
using AmazonECommerce.Application.Validators.Authentication;
using FluentValidation;

namespace AmazonECommerce.Application.Validators;

public class ValidationService : IValidationService
{
    public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
    {
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            string errorsToString = string.Join("; ", errors);
            return new ServiceResponse(Message: errorsToString);
        }

        return new ServiceResponse(true);
    }
}