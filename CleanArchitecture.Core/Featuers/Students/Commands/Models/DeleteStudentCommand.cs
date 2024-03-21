using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Models
{
    public class DeleteStudentCommand(int id) : IRequest<Response<string>>
    {
        public int Id { get; set; } = id;
    }
}
