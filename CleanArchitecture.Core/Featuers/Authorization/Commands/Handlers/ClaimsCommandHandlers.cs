using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandlers(IAuthorizationServices authorizationServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<UpdateUserClaims, Response<string>>
    {
        public async Task<Response<string>> Handle(UpdateUserClaims request, CancellationToken cancellationToken)
        {
            var result = await authorizationServices.UpdateUserClaim(request);
            switch (result)
            {
                case "NotFound": return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
                case "Failed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.notAllowed]);
                case "AddedFailed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.BadRequest]);
            }
            return Success<string>("");
        }
    }
}
