using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler(UserManager<ApplicationUser> userManager
       , SignInManager<ApplicationUser> signInManager, IAuthenticationServices authenticationServices,
       IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
            IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
            IRequestHandler<ResetPasswordCommand, Response<string>>,
            IRequestHandler<ForgetPasswordCommand, Response<string>>
    {
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null) return NotFound<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var SignInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!SignInResult.Succeeded)
                return BadRequest<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.PasswordNotCorrect]);
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.EmailConfirmed]);
            // Generate Token
            var result = await authenticationServices.GetJwtToken(user);
            return Success(result);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.SendCodeResetPassword(request.Email);
            switch (result)
            {
                case "NotFound": return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
                case "FailedUpdate": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.BadRequest]);
                case "Failed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.SendEmailFailed]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.ForgetPassword(request.Password, request.Email);
            switch (result)
            {
                case "NotFound": return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
                case "Failed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.BadRequest]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(result);
            }
        }
    }


}
