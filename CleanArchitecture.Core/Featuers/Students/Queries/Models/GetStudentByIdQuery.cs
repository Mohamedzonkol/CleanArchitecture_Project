using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Students.Queries.Results;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Students.Queries.Models
{
    public class GetStudentByIdQuery(int id) : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; } = id;
    }
}
