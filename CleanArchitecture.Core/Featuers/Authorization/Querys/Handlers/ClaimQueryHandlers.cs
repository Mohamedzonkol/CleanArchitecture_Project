using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.DTOS;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Querys.Handlers
{
    public class ClaimQueryHandlers(IAuthorizationServices authorizationServices,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<ManageUserClaimQuery, Response<ManageUserClaimResult>>
    {
        public async Task<Response<ManageUserClaimResult>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user is null) return NotFound<ManageUserClaimResult>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var result = await authorizationServices.ManageUserClaim(user);
            return Success(result);
        }
    }
}
