using AutoMapper;

namespace CleanArchitecture.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            GetSummationSalary();
        }
    }
}