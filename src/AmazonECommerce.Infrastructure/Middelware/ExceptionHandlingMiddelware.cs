using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            var innerException = ex.InnerException as SqlException;
            if (innerException != null)
            {
                switch(innerException.Number)
                {
                    case 2627: // Unique constraint violation
                        context

                }
            }
        }
    }
}