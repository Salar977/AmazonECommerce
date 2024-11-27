namespace AmazonECommerce.Application.Exceptions;

public class ItemNotFoundException(string message) : Exception(message)
{
}