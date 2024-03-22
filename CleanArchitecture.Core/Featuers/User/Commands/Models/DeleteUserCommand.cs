using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.User.Commands.Models
{
    public class DeleteUserCommand(string id) : IRequest<Response<string>>
    {
        public string Id { get; set; } = id;
    }
}
