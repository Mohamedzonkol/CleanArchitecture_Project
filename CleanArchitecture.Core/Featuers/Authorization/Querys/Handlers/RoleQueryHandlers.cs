using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Models;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Result;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.DTOS;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Handlers
{
    public class RoleQueryHandlers(IAuthorizationServices authorizationServices, IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<GetRolesListModels, Response<List<GetRolesListResponse>>>,
            IRequestHandler<GetRoleByIdModels, Response<GetRolesListResponse>>,
            IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>>
    {
        private IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>> _requestHandlerImplementation;

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

        public async Task<Response<ManageUserRoleResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user is null) return NotFound<ManageUserRoleResult>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var result = await authorizationServices.ManageUserRole(user);
            return Success(result);
        }
    }
}
