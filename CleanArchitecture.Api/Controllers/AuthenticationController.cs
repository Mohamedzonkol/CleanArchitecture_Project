using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public class AuthenticationController(IMediator mediator) : AppControllerBase
    {
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
