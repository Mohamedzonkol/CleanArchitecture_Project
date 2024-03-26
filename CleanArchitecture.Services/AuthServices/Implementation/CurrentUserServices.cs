using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Services.AuthServices.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Services.AuthServices.Implementation
{
    public class CurrentUserServices(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager) : ICurrentUserServices
    {
        public string GetUserId()
        {
            string userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaims.UserId)).Value;
            if (userId == null) throw new UnauthorizedAccessException();
            return userId;
        }
        public async Task<ApplicationUser> GetUser()
        {
            var user = await userManager.FindByIdAsync(GetUserId()).ConfigureAwait(false);
            if (user == null) throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<IList<string>> GetUserRoles()
        {
            var user = await GetUser().ConfigureAwait(false);
            return await userManager.GetRolesAsync(user).ConfigureAwait(false);
        }
    }
}
