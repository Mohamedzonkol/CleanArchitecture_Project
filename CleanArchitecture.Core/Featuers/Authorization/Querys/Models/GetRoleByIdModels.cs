using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Models
{
    public class GetRoleByIdModels(string id) : IRequest<Response<GetRolesListResponse>>
    {
        public string Id { get; set; } = id;
    }
}
