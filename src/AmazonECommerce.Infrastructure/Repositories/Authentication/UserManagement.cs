using AmazonECommerce.Application.Identity;
using AmazonECommerce.Application.Interfaces.Authentication;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AmazonECommerce.Infrastructure.Repositories.Authentication;

public class UserManagement(IRoleManagement roleManagement,
                            UserManager<AppUser> userManager,
                            AppDbContext dbContext) : IUserManagement
{
    public async Task<bool> CreateUserAsync(AppUser appUser)
    {
        AppUser? user = await GetUserByEmailAsync(appUser.Email!);
        if (user is not null) return false;

        return (await userManager.CreateAsync(appUser!, appUser!.PasswordHash!)).Succeeded;
    }

    public async Task<IEnumerable<AppUser>?> GetAllUsersAsync() =>
        await dbContext.Users.ToListAsync();

    public async Task<AppUser?> GetUserByEmailAsync(string email) =>
        await userManager.FindByEmailAsync(email);

    public Task<AppUser> GetUserByIdAsync(string id) =>
        userManager.FindByIdAsync(id)!;

    public async Task<List<Claim>> GetUserClaimsAsync(string email)
    {
        AppUser? user = await GetUserByEmailAsync(email);

        string? roleName = await roleManagement.GetUserRoleAsync(user!.Email!);

        List<Claim> claims = [
            new Claim("Fullname", user!.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, roleName!)
            ];

        return claims;
    }

    public async Task<bool> LoginUserAsync(AppUser appUser)
    {
        var user = await GetUserByEmailAsync(appUser.Email!);
        if (user is null) return false;

        string? roleName = await roleManagement.GetUserRoleAsync(user.Email!);

        if (string.IsNullOrEmpty(roleName)) return false;

        return await userManager.CheckPasswordAsync(user, appUser.PasswordHash!);

    }

    public async Task<int> RemoveUserByEmailAsync(string email)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        dbContext.Users.Remove(user!);
        return await dbContext.SaveChangesAsync();
    }
}