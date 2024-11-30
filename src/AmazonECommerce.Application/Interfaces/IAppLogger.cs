namespace AmazonECommerce.Application.Interfaces;

public interface IAppLogger<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(Exception e, string message);
}