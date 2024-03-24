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
    }
}
