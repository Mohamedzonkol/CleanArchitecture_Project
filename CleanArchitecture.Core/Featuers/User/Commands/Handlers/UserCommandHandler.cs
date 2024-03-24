using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.Entites.Idetitiy;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Handlers
{
    public class UserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager
        , IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AddUsersCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddUsersCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null) return BadRequest<string>(stringLocalizer[SheardResoursesKeys.EmailExit]);
            var userName = await userManager.FindByNameAsync(request.UserName);
            if (userName is not null) return BadRequest<string>(stringLocalizer[SheardResoursesKeys.UserNameExist]);
            //Mapping
            var Usermapping = mapper.Map<ApplicationUser>(request);
            var UserCreated = await userManager.CreateAsync(Usermapping, request.Password);
            if (!UserCreated.Succeeded) return BadRequest<string>(UserCreated.Errors.FirstOrDefault()!.Description/*stringLocalizer[SheardResoursesKeys.FaildToAddUser]*/);
            var users = await userManager.Users.ToListAsync();
            await userManager.AddToRoleAsync(Usermapping, "User");


            return Created("");
        }
    }
}
