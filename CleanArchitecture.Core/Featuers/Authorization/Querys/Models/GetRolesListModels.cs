using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Models
{
    public class GetRolesListModels : IRequest<Response<List<GetRolesListResponse>>>
    {
    }
}
