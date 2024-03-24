using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Services.Services
{
    public class AuthorizationServices(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : IAuthorizationServices
    {
        public async Task<string> AddRole(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var role = await roleManager.CreateAsync(identityRole).ConfigureAwait(false);
            if (!role.Succeeded) return "Failed";
            return "Success";
        }

        public async Task<string> UpdateRole(string roleName, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            if (role is null) return "NotFound";
            role.Name = roleName;
            var result = await roleManager.UpdateAsync(role).ConfigureAwait(false);
            if (!result.Succeeded) return result.Errors.SingleOrDefault()?.ToString() ?? "Failed";
            return "Success";

        }

        public async Task<string> DeleteRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            if (role is null) return "NotFound";
            var users = await userManager.GetUsersInRoleAsync(role.Name);
            if (users is not null && users.Count() != 0) return "Used";
            var result = await roleManager.DeleteAsync(role).ConfigureAwait(false);
            if (!result.Succeeded) return result.Errors.SingleOrDefault()?.ToString() ?? "Failed";
            return "Success";
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await roleManager.Roles.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IdentityRole?> GetRoleById(string roleId)
        {
            return await roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
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
