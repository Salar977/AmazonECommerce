using AmazonECommerce.Application.DTOs.Identity;
using AmazonECommerce.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonECommerce.Api.Controllers;
[Route("api/account")]
[ApiController]
public class AuthController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {
        var result = await authenticationService.CreateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLogin([FromBody] UserLogin userLogin)
    {
        var result = await authenticationService.UserLoginAsync(userLogin);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("refreshToken/{refreshToken}")]
    public async Task<IActionResult> ReviveToken(string refreshToken)
    {
        var result = await authenticationService.ReviveTokenAsync(refreshToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
