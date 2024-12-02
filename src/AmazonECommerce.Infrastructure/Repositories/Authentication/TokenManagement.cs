using AmazonECommerce.Application.Interfaces.Authentication;
using AmazonECommerce.Domain.Entities.Identity;
using AmazonECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AmazonECommerce.Infrastructure.Repositories.Authentication;

public class TokenManagement(AppDbContext dbContext,
                             IConfiguration config) : ITokenManagement
{
    public async Task<int> AddRefreshTokenAsync(string userId, string refreshToken)
    {
        dbContext.RefreshToken.Add(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken
        });
        return await dbContext.SaveChangesAsync();
    }

    public string GenerateToken(List<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SigningKey"]!));
        
        var cred = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetRefreshToken()
    {
        const int bytesSize = 64;
        byte[] randomBytes = new byte[bytesSize];

        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        string token = Convert.ToBase64String(randomBytes);
        return WebUtility.UrlEncode(token);
    }

    public List<Claim> GetUserClaimsFromTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        if (jwtToken != null)
            return jwtToken.Claims.ToList();
        else
            return [];
    }

    public async Task<string?> GetUserIdByRefreshTokenAsync(string refreshToken) =>
        (await dbContext.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

    public async Task<int> UpdateRefreshTokenAsync(string userId, string refreshToken)
    {
        var user = await dbContext.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken);
        if (user is null) return -1;

        user.Token = refreshToken;
        return await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var user = await dbContext.RefreshToken.FirstOrDefaultAsync(_ => _.Token == refreshToken);

        return user is not null;
    }
}