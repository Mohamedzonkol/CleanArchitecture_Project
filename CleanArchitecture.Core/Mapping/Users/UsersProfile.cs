using AutoMapper;

namespace CleanArchitecture.Core.Mapping.Users
{
    public partial class UsersProfile : Profile
    {
        public UsersProfile()
        {
            AddUsersCommandMapping();
            GetUserQueryMapping();
            GetUserByIdMapping();
        }
    }
}
