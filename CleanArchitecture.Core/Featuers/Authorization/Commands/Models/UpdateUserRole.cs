using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.DTOS;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Models
{
    public class UpdateUserRole : ManageUserRoleResult, IRequest<Response<string>>
    {
    }
}
