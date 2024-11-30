using AmazonECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace AmazonECommerce.Infrastructure.Middelware;

public class ExceptionHandlingMiddelware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(DbUpdateException ex)
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddelware>>();
            if (ex.InnerException is SqlException innerException)
            {
                logger.LogError(innerException, "Sql Exception");
                context.Response.ContentType = "application/json";
                switch (innerException.Number)
                {
                    case 2627: // Unique constraint violation
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize("Unique constraint violation"));
                        break;
                    case 547: // Foreign key constraint violation
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize("Foreign key constraint violation"));
                        break;
                    case 515: // Cannot insert null
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize("Cannot insert null"));
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize("An error occurred while processing your request."));
                        break;

                }
            }
            else
            {
                // Handle general DbUpdateException without a SQL-specific cause
                logger.LogError(ex, "Related EFCore Exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize("An error occurred while saving the entity changes"));
            }
        }
        catch (Exception ex)
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddelware>>();
            logger.LogError(ex, "Unkown Exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("An unexpected error occurred " + ex.ToString()));
        }
    }
}