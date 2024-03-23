using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager
       , SignInManager<ApplicationUser> signInManager, IAuthenticationServices authenticationServices,
       IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer), IRequestHandler<SignInCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null) return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var SignInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!SignInResult.Succeeded)
                return BadRequest<string>(stringLocalizer[SheardResoursesKeys.PasswordNotCorrect]);
            // Generate Token
            var accessToken = await authenticationServices.GetJwtToken(user);
            return Success(accessToken);

        }

    }
}
