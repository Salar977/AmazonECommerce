using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Mapping;
using AmazonECommerce.Application.Services;
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
        services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
    }
}