using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Department.Query.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentPageSize { get; set; }
        public int StudentPageNumber { get; set; }
    }
}
