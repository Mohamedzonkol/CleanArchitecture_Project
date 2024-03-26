using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Handlers
{
    public class UserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IEmailServices emailServices,
        IUserServices userServices
        , IHttpContextAccessor httpContextAccessor, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AddUsersCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddUsersCommand request, CancellationToken cancellationToken)
        {
            var Usermapping = mapper.Map<ApplicationUser>(request);
            var UserCreated = await userServices.AddUserAsync(Usermapping, request.Password);
            switch (UserCreated)
            {
                case "EmailIsExist": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.EmailExit]);
                case "UserNameIsExist": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.UserNameExist]);
                case "ErrorInCreateUser": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<string>(stringLocalizer[SheardResoursesKeys.BadRequest]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(UserCreated);
            }

        }
    }
}
