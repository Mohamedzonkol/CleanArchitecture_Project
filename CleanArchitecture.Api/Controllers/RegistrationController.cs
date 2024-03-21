using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.User.Commands.Models;
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
    }
}
