using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Services.Abstract
{
    public interface IUserServices
    {
        Task<string> AddUserAsync(ApplicationUser user, string password);
    }
}
