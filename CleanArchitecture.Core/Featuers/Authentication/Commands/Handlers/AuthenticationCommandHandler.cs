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
            IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null) return NotFound<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var SignInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!SignInResult.Succeeded)
                return BadRequest<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.PasswordNotCorrect]);
            // Generate Token
            var result = await authenticationServices.GetJwtToken(user);
            return Success(result);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = authenticationServices.readJwt(request.AccessToken);
            var (userId, expiryDate) =
               await authenticationServices.ValidateTokenDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userId)
            {
                case ("AlgorithmsIsWrong"):
                    return Unauthorized<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.AlgorithmsIsWrong]);
                    break;
                case ("TokenIsNotExpired"):
                    return BadRequest<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.TokenIsNotExpired]);
                    break;
                case ("RefreshTokenIsNotFound"):
                    return NotFound<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.RefreshTokenIsNotFound]);
                    break;
                case ("RefreshTokenIsExpired"):
                    return Unauthorized<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.RefreshTokenIsExpired]);
                    break;
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound<JwtAuthResult>();
            var result = await authenticationServices.GetRefreshToken(user, request.RefreshToken, expiryDate, jwtToken);
            return Success(result);
        }
    }


}
