using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Department.Query.Models
{
    public class GetDepartmentStudentQuery : IRequest<Response<List<GetDepartmentStudentResult>>>
    {
    }
}
