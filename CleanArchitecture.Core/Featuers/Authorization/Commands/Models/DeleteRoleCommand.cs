using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Models
{
    public class DeleteRoleCommand(string id) : IRequest<Response<string>>
    {
        public string Id { get; set; } = id;
    }
}
