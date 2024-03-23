using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Services.Services
{
    public class AuthenticationServices(JwtSettings jwtSettings, IRefreshTokenReporesatory refreshTokenReporesatory,
        UserManager<ApplicationUser> userManager) : IAuthenticationServices
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens = new();

        public async Task<JwtAuthResult> GetJwtToken(ApplicationUser user)
        {
            var (JwtToken, accessToken) = generateJwtSecurityToken(user);
            RefreshToken refreshToken = GetRefreshToken(user.UserName);
            UserRefreshToken userRefreashToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsExpired = false,
                JwtId = JwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await refreshTokenReporesatory.AddAsync(userRefreashToken).ConfigureAwait(false);
            var response = new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken)
        {
            var token = readJwt(AccessToken);
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                throw new SecurityTokenException("Algorithms Is Wrong");
            if (token.ValidTo > DateTime.UtcNow)
                throw new SecurityTokenException("Token Is Not Expired");
            string userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.UserId)).Value;
            var userRefreshToken = await refreshTokenReporesatory.GetTableNoTracking().FirstOrDefaultAsync(x =>
                x.Token == AccessToken &&
                x.RefreshToken == RefreshToken && x.UserId == userId).ConfigureAwait(false);
            if (userRefreshToken == null)
                throw new SecurityTokenException("Refresh Token Is  Not Found");
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsExpired = true;
                userRefreshToken.IsUsed = false;
                await refreshTokenReporesatory.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh Token Is  Expired");
            }

            var user = await userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user is null)
                throw new SecurityTokenException("User Is Not Found");
            var (jwtSecurityToken, newToken) = generateJwtSecurityToken(user);
            var response = new JwtAuthResult();
            var refreshTokenResult = new RefreshToken();
            response.AccessToken = newToken;
            refreshTokenResult.UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.UserName)).Value;
            refreshTokenResult.TokenString = RefreshToken;
            refreshTokenResult.ExpireAt = userRefreshToken.ExpiryDate;
            response.RefreshToken = refreshTokenResult;
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { jwtSettings.Issuer },
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
            };
            var valditor = handler.ValidateToken(accessToken, parameters, out SecurityToken Security);

            try
            {
                if (valditor is null) throw new SecurityTokenException("Invalid Token");
                return "Not Expired";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        private (JwtSecurityToken, string) generateJwtSecurityToken(ApplicationUser user)
        {
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                getClaims(user),
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }
        private JwtSecurityToken readJwt(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
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
            {  new Claim(nameof(UserClaims.UserId),user.Id),
                new Claim(nameof(UserClaims.UserName),user.UserName),
                new Claim(nameof(UserClaims.Email),user.Email ),
                new Claim(nameof(UserClaims.PhoneNumber),user.PhoneNumber?? string.Empty),
            };
            return claims;
        }
    }
}
