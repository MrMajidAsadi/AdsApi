using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ads.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Ads.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Identity;

public class IdentityTokenClaimService : ITokenClaimService
{
    private readonly UserManager<AdsAppUser> _userManager;
    private readonly IRepository<User> _userRepository;

    public IdentityTokenClaimService(
        UserManager<AdsAppUser> userManager,
        IRepository<User> userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<string> GetToken(string userName)
    {
        string output = "";

        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(Ads.Core.Constants.Authorization.JWT_SECRET_KEY);

        var user = await _userManager.FindByNameAsync(userName);
        var internalUser = await _userRepository.GetAll()
            .SingleOrDefaultAsync(u => u.IdentityId == user.Id);
            
        var roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

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