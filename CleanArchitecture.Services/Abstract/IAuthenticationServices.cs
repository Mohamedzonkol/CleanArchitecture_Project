using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJwtToken(ApplicationUser user);

        public Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, string refreshToken, DateTime? expiryDate,
            JwtSecurityToken token);
        public Task<string> ValidateToken(string AccessToken);
        public Task<(string, DateTime?)> ValidateTokenDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        public JwtSecurityToken readJwt(string accessToken);

    }
}
