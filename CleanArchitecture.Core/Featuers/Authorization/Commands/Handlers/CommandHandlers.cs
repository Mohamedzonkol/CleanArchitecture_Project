using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authorization.Commands.Handlers
{
    public class CommandHandlers(IAuthorizationServices authorizationServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AddRoleCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await authorizationServices.AddRole(request.RoleName);
            if (role == "Failed") return BadRequest<string>(stringLocalizer[SheardResoursesKeys.BadRequest]);
            return Success("");
        }
    }
}
