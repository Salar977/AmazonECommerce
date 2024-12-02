using Microsoft.AspNetCore.Identity;

namespace AmazonECommerce.Application.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}