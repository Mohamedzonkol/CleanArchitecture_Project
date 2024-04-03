using AutoMapper;
using CleanArchitecture.Core.Featuers.User.Query.Result;
using CleanArchitecture.Date.Entites.Idetitiy;


namespace CleanArchitecture.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public void GetSummationSalary()
        {
            CreateMap<ApplicationUser, GetUserListResponse>();

        }
    }
}
