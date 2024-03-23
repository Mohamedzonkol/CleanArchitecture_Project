using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthenticationServices
    {
        public JwtAuthResult GetJwtToken(ApplicationUser user);
    }
}
