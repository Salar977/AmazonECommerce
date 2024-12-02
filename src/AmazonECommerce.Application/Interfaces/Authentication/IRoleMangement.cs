using AmazonECommerce.Application.Identity;

namespace AmazonECommerce.Application.Interfaces.Authentication;

public interface IRoleManagement
{
    Task<string?> GetUserRoleAsync(string userEmail);
    Task<bool> AddUserToRoleAsync(AppUser appUser, string roleName);
}
