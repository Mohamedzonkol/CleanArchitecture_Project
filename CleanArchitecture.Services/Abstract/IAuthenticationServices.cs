using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJwtToken(ApplicationUser user);
        public Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken);
        public Task<string> ValidateToken(string AccessToken);
        public Task<string> ConfirmEmail(string userId, string code);
        public Task<string> SendCodeResetPassword(string Email);
        public Task<string> ResetPassword(string code, string email);
        public Task<string> ForgetPassword(string password, string email);
    }
}
