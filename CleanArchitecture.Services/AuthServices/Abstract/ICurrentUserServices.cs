using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Services.AuthServices.Abstract
{
    public interface ICurrentUserServices
    {
        string GetUserId();
        Task<ApplicationUser> GetUser();
        Task<IList<string>> GetUserRoles();
    }
}
