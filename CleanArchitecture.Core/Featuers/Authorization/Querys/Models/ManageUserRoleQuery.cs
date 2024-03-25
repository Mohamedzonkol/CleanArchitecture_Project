using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.DTOS;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Models
{
    public class ManageUserRoleQuery(string id) : IRequest<Response<ManageUserRoleResult>>
    {
        public string UserId { get; set; } = id;
    }
}
