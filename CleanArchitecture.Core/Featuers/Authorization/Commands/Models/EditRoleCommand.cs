using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Models
{
    public class EditRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
