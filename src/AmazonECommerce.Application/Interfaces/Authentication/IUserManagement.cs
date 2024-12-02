using AmazonECommerce.Application.Identity;
using System.Security.Claims;

namespace AmazonECommerce.Application.Interfaces.Authentication;

public interface IUserManagement
{
    Task<bool> CreateUserAsync(AppUser appUser);
    Task<bool> LoginUserAsync(AppUser appUser);
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<AppUser> GetUserByIdAsync(string id);
    Task<IEnumerable<AppUser>?> GetAllUsersAsync();
    Task<int> RemoveUserByEmailAsync(string email);
    Task<List<Claim>> GetUserClaimsAsync(string email);
}
