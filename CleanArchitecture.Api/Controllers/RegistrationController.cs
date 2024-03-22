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
        public async Task<IActionResult> GetAllUsers([FromRoute] string id)
        {
            var response = await mediator.Send(new GetUseByIdQuery(id));
            return Ok(response);
        }
    }
}
