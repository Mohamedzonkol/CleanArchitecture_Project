using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.User.Commands.Models;
using CleanArchitecture.Core.Featuers.User.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public class RegistrationController(IMediator mediator) : AppControllerBase
    {
        [HttpPost("CreateUser")]
        public async Task<IActionResult> GetDepartmentById([FromBody] AddUsersCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUserListQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var response = await mediator.Send(new GetUseByIdQuery(id));
            return Ok(response);
        }
        [HttpPut("EditUser")]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
