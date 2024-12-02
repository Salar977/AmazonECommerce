using AmazonECommerce.Application.Identity;
using AmazonECommerce.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AmazonECommerce.Infrastructure.Repositories.Authentication;

public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
{
    public async Task<bool> AddUserToRoleAsync(AppUser appUser, string roleName) =>
        (await userManager.AddToRoleAsync(appUser, roleName)).Succeeded;

    public async Task<string?> GetUserRoleAsync(string userEmail)
    {
        var user = await userManager.FindByEmailAsync(userEmail);

        return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
    }
}