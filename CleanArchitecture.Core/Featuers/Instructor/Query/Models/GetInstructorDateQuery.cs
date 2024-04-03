using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.Entites.Function;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Instructor.Query.Models
{
    public class GetInstructorDateQuery : IRequest<Response<List<GetInstructorDataResult>>>
    {
    }
}
