using CleanArchitecture.Core.Featuers.Students.Commands.Models;

namespace CleanArchitecture.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Date.Entites.Student>()
                .ForMember(des => des.DID, ops => ops.MapFrom(
                    src => src.DepartmentId))
                .ForMember(des => des.NameAr, ops => ops.MapFrom(
                    src => src.NameAr))
                .ForMember(des => des.NameEn, ops => ops.MapFrom(
                    src => src.NameEn));
        }
    }
}
