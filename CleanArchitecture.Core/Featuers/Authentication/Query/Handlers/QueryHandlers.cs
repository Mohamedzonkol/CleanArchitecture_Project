using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Query.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Query.Handlers
{
    public class QueryHandlers(IAuthenticationServices authenticationServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AuthuorirzeQueryModel, Response<string>>,
            IRequestHandler<ConfirmEmailQuery, Response<string>>,
            IRequestHandler<ResetPasswordQuery, Response<string>>
    {
        public async Task<Response<string>> Handle(AuthuorirzeQueryModel request, CancellationToken cancellationToken)
        {
            var accessToken = await authenticationServices.ValidateToken(request.AccessToken);
            if (accessToken == "Not Expired") return Success("This  Token Is Still Valid");
            return BadRequest<string>("This access Token Is Expired");
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var emailConfirm = await authenticationServices.ConfirmEmail(request.UserId, request.Code);
            if (emailConfirm == "Failed") return BadRequest<string>(stringLocalizer[SheardResoursesKeys.FaildEmailConfirmed]);
            return Success("Confirm Email Is Done");
        }

        public async Task<Response<string>> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.ResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "NotFound": return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
                case "Failed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.SendEmailFailed]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }
    }
}
