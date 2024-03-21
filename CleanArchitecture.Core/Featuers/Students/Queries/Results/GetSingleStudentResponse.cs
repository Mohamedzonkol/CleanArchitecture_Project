using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Students.Queries.Models;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Students.Queries.Results
{
    public class GetSingleStudentResponse : IRequest<Response<GetStudentByIdQuery>>
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? Department { get; set; }
    }
}
