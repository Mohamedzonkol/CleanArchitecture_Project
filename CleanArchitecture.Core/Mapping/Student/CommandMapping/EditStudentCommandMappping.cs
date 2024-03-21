using CleanArchitecture.Core.Featuers.Students.Commands.Models;

namespace CleanArchitecture.Core.Mapping.Student
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMappping()
        {
            CreateMap<EditStudentCommand, Date.Entites.Student>()
                .ForMember(des => des.DID, ops => ops.MapFrom(
                    src => src.DepartmentId))
                .ForMember(des => des.StudID, ops => ops.MapFrom(
                    src => src.Id))
                .ForMember(des => des.NameAr, ops => ops.MapFrom(
                    src => src.NameAr))
                .ForMember(des => des.NameEn, ops => ops.MapFrom(
                    src => src.NameEn));

        }
    }
}
