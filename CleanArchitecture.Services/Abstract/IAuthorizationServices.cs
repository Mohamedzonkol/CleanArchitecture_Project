using CleanArchitecture.Date.DTOS;
using CleanArchitecture.Date.Entites.Idetitiy;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthorizationServices
    {
        Task<string> AddRole(string roleName);
        Task<string> UpdateRole(string roleName, string roleId);
        Task<string> DeleteRole(string roleId);
        Task<List<IdentityRole>> GetRoles();
        Task<IdentityRole?> GetRoleById(string roleId);
        Task<bool> IsRoleNameExist(string Name);
        Task<ManageUserRoleResult> ManageUserRole(ApplicationUser user);
        Task<string> UpdateUserRole(ManageUserRoleResult request);
        Task<string> UpdateUserClaim(ManageUserClaimResult request);
        Task<ManageUserClaimResult> ManageUserClaim(ApplicationUser user);

    }
}
