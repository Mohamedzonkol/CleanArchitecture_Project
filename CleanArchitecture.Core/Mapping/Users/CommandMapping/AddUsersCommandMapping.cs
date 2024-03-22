using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Date.Entites.Idetitiy;

namespace CleanArchitecture.Core.Mapping.Users
{
    public partial class UsersProfile
    {
        public void AddUsersCommandMapping()
        {
            CreateMap<AddUsersCommand, ApplicationUser>();
            //.ForMember(des => des.UserName, ops => ops.MapFrom(
            //    src => src.UserName))
            //.ForMember(des => des.PhoneNumber, ops => ops.MapFrom(
            //    src => src.PhoneNumber));
        }
    }
}
