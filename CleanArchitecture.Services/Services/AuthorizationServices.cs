using CleanArchitecture.Date.DTOS;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArchitecture.Services.Services
{
    public class AuthorizationServices(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context) : IAuthorizationServices
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
            return await roleManager.RoleExistsAsync(roleName).ConfigureAwait(false);
        }

        public async Task<ManageUserRoleResult> ManageUserRole(ApplicationUser user)
        {
            var response = new ManageUserRoleResult();
            var rolesList = new List<Role>();

            var roles = await roleManager.Roles.ToListAsync().ConfigureAwait(false);
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new Role();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await userManager.IsInRoleAsync(user, role.Name)) userrole.HasRole = true;
                else userrole.HasRole = false;
                rolesList.Add(userrole);

            }

            response.Roles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserRole(ManageUserRoleResult request)
        {
            var trans = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var user = await userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);
                if (user is null) return "NotFound";
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var removeing = await userManager.RemoveFromRolesAsync(user, userRoles).ConfigureAwait(false);
                if (!removeing.Succeeded) return "Failed";
                var selectedRole = request.Roles.Where(x => x.HasRole == true)
                    .Select(x => x.Name);
                var result = await userManager.AddToRolesAsync(user, selectedRole).ConfigureAwait(false);
                if (!result.Succeeded) return "AddedFailed";
                await trans.CommitAsync().ConfigureAwait(false);
                return "Success";

            }
            catch (Exception e)
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                return "AddedFailed";
            }
        }

        public async Task<string> UpdateUserClaim(ManageUserClaimResult request)
        {
            var trans = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var user = await userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);
                if (user is null) return "NotFound";
                var userClaims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
                var removeing = await userManager.RemoveClaimsAsync(user, userClaims).ConfigureAwait(false);
                if (!removeing.Succeeded) return "Failed";
                var selectedRole = request.UserClaims.Where(x => x.Value == true)
                    .Select(x => new Claim(x.ClaimType, x.Value.ToString()));
                var result = await userManager.AddClaimsAsync(user, selectedRole).ConfigureAwait(false);
                if (!result.Succeeded) return "AddedFailed";
                await trans.CommitAsync().ConfigureAwait(false);
                return "Success";

            }
            catch (Exception e)
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                return "AddedFailed";
            }
        }

        public async Task<ManageUserClaimResult> ManageUserClaim(ApplicationUser user)
        {
            var response = new ManageUserClaimResult();
            response.UserId = user.Id;
            var claimList = new List<UserClaim>();
            var userClaims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
            foreach (var claim in ClaimsStore.Claims)
            {
                var userClaim = new Date.DTOS.UserClaim();
                userClaim.ClaimType = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                claimList.Add(userClaim);
            }
            response.UserClaims = claimList;
            return response;

        }

    }
}
