using CleanArchitecture.Core.Featuers.User.Query.Result;
using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Core.Mapping.Users
{
    public partial class UsersProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<ApplicationUser, GetUserByIdResponse>();

        }
    }
}
