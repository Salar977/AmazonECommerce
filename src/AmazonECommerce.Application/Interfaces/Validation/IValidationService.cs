using AmazonECommerce.Application.DTOs;
using FluentValidation;

namespace AmazonECommerce.Application.Interfaces.Validation;

public interface IValidationService
{
    Task<ServiceResponse> ValidateAsync<T>(T Model, IValidator<T> validator);
}