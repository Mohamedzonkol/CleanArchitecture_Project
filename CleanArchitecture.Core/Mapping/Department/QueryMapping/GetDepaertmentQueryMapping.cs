using CleanArchitecture.Core.Featuers.Department.Query.Result;
using CleanArchitecture.Date.Entites;

namespace CleanArchitecture.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void GetDepaertmentQueryMapping()
        {
            CreateMap<Date.Entites.Department, GetDepartmentByIdResponse>()
                .ForMember(des => des.ManagerName, ops => ops.MapFrom(
                    src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                .ForMember(des => des.Name, ops => ops.MapFrom(
                    src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.Id, ops => ops.MapFrom(
                    src => src.DID))
                .ForMember(des => des.SubjectList, ops => ops.MapFrom(
                    src => src.DepartmentSubjects))
                //   .ForMember(des => des.StudentList, ops => ops.MapFrom(
                //     src => src.Students))
                .ForMember(des => des.InstructorList, ops => ops.MapFrom(
                    src => src.Instructors));

            CreateMap<DepartmetSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            //CreateMap<Date.Entites.Student, StudentResponse>()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));


        }
    }

}
