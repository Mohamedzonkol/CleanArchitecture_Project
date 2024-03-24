namespace CleanArchitecture.Services.Abstract
{
    public interface IAuthorizationServices
    {
        Task<string> AddRole(string roleName);
        Task<bool> IsRoleNameExist(string roleName);
    }
}
