using CleanArchitecture.Core.Featuers.Students.Queries.Results;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Date.Helpers;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Students.Queries.Models
{
    public class GetStudentPaginatedQuery : IRequest<PaginatedResult<GetStudentPaginatedResponse>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
