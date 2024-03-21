using CleanArchitecture.Core.Featuers.Students.Queries.Results;

namespace CleanArchitecture.Core.Mapping.Student //Note That Must be The same name space in StudentProfile

{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Date.Entites.Student, GetSingleStudentResponse>()
                .ForMember(des => des.Department, ops => ops.MapFrom(
                    src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(des => des.Name, ops => ops.MapFrom(
                    src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
