using AmazonECommerce.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AmazonECommerce.Infrastructure.Services;

public class SeriLogLoggerAdapter<T>(ILogger<T> logger) : IAppLogger<T>
{
    public void LogError(Exception e, string message) => logger.LogError(e, message);

    public void LogInformation(string message) => logger.LogInformation(message);

    public void LogWarning(string message) => logger.LogWarning(message);
}