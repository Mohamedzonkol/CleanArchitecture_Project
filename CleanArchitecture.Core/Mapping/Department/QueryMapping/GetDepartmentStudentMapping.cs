using CleanArchitecture.Core.Featuers.Department.Query.Result;
using CleanArchitecture.Date.Entites.Views;

namespace CleanArchitecture.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentResult>()
                .ForMember(des => des.Name, ops => ops.MapFrom(
                    src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.StudentCount, ops => ops.MapFrom(
                    src => src.StudentCount));

        }
    }
}
