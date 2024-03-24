using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Services.Services
{
    public class AuthorizationServices(RoleManager<IdentityRole> roleManager) : IAuthorizationServices
    {
        public async Task<string> AddRole(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var role = await roleManager.CreateAsync(identityRole).ConfigureAwait(false);
            if (!role.Succeeded) return "Failed";
            return "Success";
        }

        public async Task<bool> IsRoleNameExist(string roleName)
        {
            //var role = await roleManager.FindByNameAsync(roleName).ConfigureAwait(false);
            //if(role is null) return false;
            //return true;
            return await roleManager.RoleExistsAsync(roleName).ConfigureAwait(false);
        }
    }
}
