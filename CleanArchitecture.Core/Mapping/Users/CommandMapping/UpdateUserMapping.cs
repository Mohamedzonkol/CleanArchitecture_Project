using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Core.Mapping.Users
{
    public partial class UsersProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>();

        }
    }
}
