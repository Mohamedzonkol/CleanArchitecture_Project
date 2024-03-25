using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.DTOS;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Models
{
    public class ManageUserClaimQuery(string id) : IRequest<Response<ManageUserClaimResult>>
    {
        public string UserId { get; set; } = id;
    }
}
