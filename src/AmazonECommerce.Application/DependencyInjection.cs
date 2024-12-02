using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Authentication;
using AmazonECommerce.Application.Interfaces.Validation;
using AmazonECommerce.Application.Mapping;
using AmazonECommerce.Application.Services;
using AmazonECommerce.Application.Services.Authentication;
using AmazonECommerce.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmazonECommerce.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddValidatorsFromAssemblyContaining<CategoryService>();
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}