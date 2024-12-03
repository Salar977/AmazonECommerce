using AmazonECommerce.Application.DTOs;
using AmazonECommerce.Application.DTOs.Identity;
using AmazonECommerce.Application.Identity;
using AmazonECommerce.Application.Interfaces;
using AmazonECommerce.Application.Interfaces.Authentication;
using AmazonECommerce.Application.Interfaces.Validation;
using AutoMapper;
using FluentValidation;

namespace AmazonECommerce.Application.Services;

public class AuthenticationService(ITokenManagement tokenManagement,
                                   IUserManagement userManagement,
                                   IRoleManagement roleManagement,
                                   IAppLogger<AuthenticationService> logger,
                                   IMapper mapper, IValidator<UserRequest> userRequestValidator,
                                   IValidator<UserLogin> userLoginValidator,
                                   IValidationService validationService) : IAuthenticationService
{
    public async Task<ServiceResponse> CreateUserAsync(UserRequest userRequest)
    {
        var validationResult = await validationService.ValidateAsync(userRequest, userRequestValidator);
        if (!validationResult.Success) return validationResult;

        var mappedModel = mapper.Map<AppUser>(userRequest);
        mappedModel.UserName = userRequest.Email;
        mappedModel.PasswordHash = userRequest.Password;

        var result = await userManagement.CreateUserAsync(mappedModel);
        if (!result)
            return new ServiceResponse(Message: "Email Address might be already in use or unknown error occurred");

        var user = await userManagement.GetUserByEmailAsync(userRequest.Email);
        var users = await userManagement.GetAllUsersAsync();
        var role = users!.Count() > 1 ? "User" : "Admin";

        bool assignedResult = await roleManagement.AddUserToRoleAsync(user!, role);

        if (!assignedResult)
        {
            // remove user
            int removeUserResult = await userManagement.RemoveUserByEmailAsync(user!.Email!);
            if (removeUserResult <= 0)
            {
                logger.LogError(
                    new Exception($"User with email {user.Email} failed to be removed as a result of role assigning issue"),
                    "User could not be assigned role");
                return new ServiceResponse(Message: "Error occurred in creating account.");
            }
        }

        // TODO: Verify email

        logger.LogInformation($"The account {user!.FullName} was created with the role {role}");
        return new ServiceResponse(true, "Account created!");
    }

    public async Task<LoginResponse> UserLoginAsync(UserLogin userLogin)
    {
        var validationResult = await validationService.ValidateAsync(userLogin, userLoginValidator);
        if (!validationResult.Success)
            return new LoginResponse(Message: validationResult.Message);

        var mappedModel = mapper.Map<AppUser>(userLogin);
        mappedModel.PasswordHash = userLogin.Password;

        bool loginResult = await userManagement.LoginUserAsync(mappedModel);
        if (!loginResult)
            return new LoginResponse(Message: "Email not found or invalid credentials");

        var user = await userManagement.GetUserByEmailAsync(userLogin.Email);
        var claims = await userManagement.GetUserClaimsAsync(userLogin.Email);

        string jwtToken = tokenManagement.GenerateToken(claims);
        string refreshToken = tokenManagement.GetRefreshToken();

        int saveTokenResult = 0;
        bool userTokenCheck = await tokenManagement.ValidateRefreshTokenAsync(refreshToken);
        if (userTokenCheck)
            await tokenManagement.UpdateRefreshTokenAsync(user!.Id, refreshToken);
        else
            saveTokenResult = await tokenManagement.AddRefreshTokenAsync(user!.Id, refreshToken);

        return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error occurred while authenticating") :
            new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);
    }

    public async Task<LoginResponse> ReviveTokenAsync(string refreshToken)
    {
        bool validateTokenResult = await tokenManagement.ValidateRefreshTokenAsync(refreshToken);
        if (!validateTokenResult)
            return new LoginResponse(Message: "Invalid token");

        string userId = (await tokenManagement.GetUserIdByRefreshTokenAsync(refreshToken))!;
        AppUser? user = await userManagement.GetUserByIdAsync(userId!);
        var claims = await userManagement.GetUserClaimsAsync(user.Email!);

        string newJwtToken = tokenManagement.GenerateToken(claims);
        string newRefreshToken = tokenManagement.GetRefreshToken();
        await tokenManagement.UpdateRefreshTokenAsync(userId, newRefreshToken);

        return new LoginResponse(Success: true, Token: newJwtToken, RefreshToken: newRefreshToken);
    }
}