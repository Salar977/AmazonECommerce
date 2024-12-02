namespace AmazonECommerce.Application.DTOs.Identity;

public class UserLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}