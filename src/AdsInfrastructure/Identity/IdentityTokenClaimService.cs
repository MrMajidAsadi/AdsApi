using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ads.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Ads.Infrastructure.Identity;

public class IdentityTokenClaimService : ITokenClaimService
{
    private readonly UserManager<AdsAppUser> _userManager;

    public IdentityTokenClaimService(UserManager<AdsAppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GetToken(string userName)
    {
        string output = "";

        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(Ads.Core.Constants.Authorization.JWT_SECRET_KEY);

        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new() { new Claim(ClaimTypes.Name, userName) };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        output = tokenHandler.WriteToken(token);

        return output;
    }
}