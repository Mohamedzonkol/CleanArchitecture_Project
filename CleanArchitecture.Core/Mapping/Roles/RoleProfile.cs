using AutoMapper;

namespace CleanArchitecture.Core.Mapping.Roles
{
    public partial class RoleProfile : Profile
    {
        public RoleProfile()
        {
            GetRoleMapping();
        }
    }
}
