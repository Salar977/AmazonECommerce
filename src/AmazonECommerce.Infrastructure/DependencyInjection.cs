using AmazonECommerce.Application.Identity;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Authentication;
using AmazonECommerce.Application.Interfaces.Cart;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Infrastructure.Data;
using AmazonECommerce.Infrastructure.Middelware;
using AmazonECommerce.Infrastructure.Repositories;
using AmazonECommerce.Infrastructure.Repositories.Authentication;
using AmazonECommerce.Infrastructure.Repositories.Cart;
using AmazonECommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.AddScoped<IUserManagement, UserManagement>();
        services.AddScoped<IRoleManagement, RoleManagement>();
        services.AddScoped<ITokenManagement, TokenManagement>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
            options.DefaultScheme =
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = config["Jwt:Audience"],
                ValidIssuer = config["Jwt:Issuer"],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SigningKey"]!))
            };
        });

        Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

    }

    public static void UseInfrastructureService(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddelware>();
    }
}