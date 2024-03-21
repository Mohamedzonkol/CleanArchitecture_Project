using AutoMapper;

namespace CleanArchitecture.Core.Mapping.Student
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentByIdMapping();
            AddStudentCommandMapping();
            EditStudentCommandMappping();
        }
    }
}
