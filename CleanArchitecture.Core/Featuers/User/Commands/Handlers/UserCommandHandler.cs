using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Date.Entites.Idetitiy;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Handlers
{
    public class UserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager
        , IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AddUsersCommand, Response<string>>,
            IRequestHandler<UpdateUserCommand, Response<string>>,
            IRequestHandler<DeleteUserCommand, Response<string>>
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
            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await userManager.FindByIdAsync(request.Id);
            if (oldUser is null) return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var newUser = mapper.Map(request, oldUser);
            var result = await userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return BadRequest<string>(result.Errors.SingleOrDefault()!.Description);
            return Success("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user is null) return NotFound<string>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>(result.Errors.SingleOrDefault()!.Description);
            return Success("");
        }
    }
}
