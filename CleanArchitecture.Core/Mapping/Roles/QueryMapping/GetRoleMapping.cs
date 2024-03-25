using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleMapping()
        {
            CreateMap<IdentityRole, GetRolesListResponse>();
        }
    }

}
