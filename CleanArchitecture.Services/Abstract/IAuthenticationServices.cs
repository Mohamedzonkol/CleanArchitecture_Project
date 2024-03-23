using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJwtToken(ApplicationUser user);
        public Task<JwtAuthResult> GetRefreshToken(string AccessToken,string RefreshToken);
        public Task<string> ValidateToken(string AccessToken);
    }
}
