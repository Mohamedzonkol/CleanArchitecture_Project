using CleanArchitecture.Core.Featuers.Department.Query.Models;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using CleanArchitecture.Date.Entites.Procedures;

namespace CleanArchitecture.Core.Mapping.Department
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentProcQueryMapping()
        {
            CreateMap<GetDepartmentStudentProcQuery, DepartmentCountProcParameter>();

            CreateMap<ViewDepartmentCountProc, GetDepartmentStudentProcResult>()
                .ForMember(des => des.Name, ops => ops.MapFrom(
                    src => src.Localize(src.DNameAr, src.DNameEn)))
                ;

        }
    }
}

