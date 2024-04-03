using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Instructor.Query.Models
{
    public class GetSummationSalaryQuery : IRequest<Response<decimal>>
    {
    }
}
