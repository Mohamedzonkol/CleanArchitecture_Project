using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.DTOS;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Models
{
    public class UpdateUserClaims : ManageUserClaimResult, IRequest<Response<string>>
    {
    }
}
