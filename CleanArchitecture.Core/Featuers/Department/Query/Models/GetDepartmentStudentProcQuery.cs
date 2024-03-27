using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Department.Query.Models
{
    public class GetDepartmentStudentProcQuery(int id) : IRequest<Response<GetDepartmentStudentProcResult>>
    {
        public int DID { get; set; } = id;
    }
}
