using Auth.Domain;
using Auth.Domain.Abstractions.Services;
using Auth.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> Login(UserLogin userLogin)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Email, userLogin.Email) };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: new SigningCredentials(AuthOptions.SymmetricKey, SecurityAlgorithms.HmacSha256));

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(jwt);
        }
    }
}
