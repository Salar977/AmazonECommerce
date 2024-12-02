using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Identity;

namespace AmazonECommerce.Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResponse> CreateUserAsync(UserRequest userRequest);
    Task<LoginResponse> UserLoginAsync(UserLogin userLogin);
    Task<LoginResponse> ReviveTokenAsync(string refreshToken);
}