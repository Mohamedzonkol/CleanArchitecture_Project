using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Models;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Handlers
{
    public class QueryHandlers(IAuthorizationServices authorizationServices, IMapper mapper,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<GetRolesListModels, Response<List<GetRolesListResponse>>>,
            IRequestHandler<GetRoleByIdModels, Response<GetRolesListResponse>>
    {
        public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListModels request, CancellationToken cancellationToken)
        {
            var roles = await authorizationServices.GetRoles();
            var result = mapper.Map<List<GetRolesListResponse>>(roles);
            return Success(result);

        }

        public async Task<Response<GetRolesListResponse>> Handle(GetRoleByIdModels request, CancellationToken cancellationToken)
        {
            var role = await authorizationServices.GetRoleById(request.Id);
            if (role is null) return NotFound<GetRolesListResponse>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var result = mapper.Map<GetRolesListResponse>(role);
            return Success(result);
        }
    }
}
