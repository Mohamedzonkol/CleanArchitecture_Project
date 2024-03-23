using AutoMapper;
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
    public class AuthenticationCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager
       , SignInManager<ApplicationUser> signInManager, IAuthenticationServices authenticationServices,
       IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer), IRequestHandler<SignInCommand, Response<JwtAuthResult>>
    {
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null) return NotFound<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var SignInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!SignInResult.Succeeded)
                return BadRequest<JwtAuthResult>(stringLocalizer[SheardResoursesKeys.PasswordNotCorrect]);
            // Generate Token
            var result = authenticationServices.GetJwtToken(user);
            return Success(result);

        }

    }
}
