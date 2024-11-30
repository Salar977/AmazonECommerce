using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using AmazonECommerce.Infrastructure.Middelware;
using AmazonECommerce.Infrastructure.Repositories;
using AmazonECommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmazonECommerce.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default"),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

        services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
        services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
        services.AddScoped(typeof(IAppLogger<>), typeof(SeriLogLoggerAdapter<>));

    }

    public static void UseInfrastructureService(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddelware>();
    }
}