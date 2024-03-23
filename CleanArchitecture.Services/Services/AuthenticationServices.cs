using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Services.Services
{
    public class AuthenticationServices(JwtSettings jwtSettings) : IAuthenticationServices
    {
        public async Task<string> GetJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaims.UserName),user.UserName),
                new Claim(nameof(UserClaims.Email),user.Email ),
                new Claim(nameof(UserClaims.PhoneNumber),user.PhoneNumber?? string.Empty),
            };
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }
    }
}
