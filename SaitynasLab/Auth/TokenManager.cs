using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SaitynasLab.Auth
{
    public interface ITokenManager
    {
        Task<string> CreateAccessTokenAsync(IdentityUser user);
    }

    public class TokenManager : ITokenManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _issuer;
        private readonly string _audience;
        private SymmetricSecurityKey _authSigningKey;

        public TokenManager(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _userManager = userManager;
            _issuer = configuration["JWT:ValidIssuer"];
            _audience = configuration["JWT:ValidAudience"];
        }

        public async Task<string> CreateAccessTokenAsync(IdentityUser user)
        {
            // List of user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                // Makes every token more unique
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim("UserId", user.Id.ToString()),
            };

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Creating the token
            var accessSecurityToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(accessSecurityToken);
        }
    }
}
