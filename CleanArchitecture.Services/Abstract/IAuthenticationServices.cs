using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthenticationServices
    {
        Task<string> GetJwtToken(ApplicationUser user);
    }
}
