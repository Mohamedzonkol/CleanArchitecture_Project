using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Services.Services
{
    public class AuthenticationServices(JwtSettings jwtSettings) : IAuthenticationServices
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens = new();

        public JwtAuthResult GetJwtToken(ApplicationUser user)
        {
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                getClaims(user),
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = GetRefreshToken(user.UserName)
            };
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            _userRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[20];
            RandomNumberGenerator randomNumberGenertor = RandomNumberGenerator.Create();
            randomNumberGenertor.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private List<Claim> getClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaims.UserName),user.UserName),
                new Claim(nameof(UserClaims.Email),user.Email ),
                new Claim(nameof(UserClaims.PhoneNumber),user.PhoneNumber?? string.Empty),
            };
            return claims;
        }
    }
}
