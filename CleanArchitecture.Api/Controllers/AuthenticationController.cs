using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Commands.Models;
using CleanArchitecture.Core.Featuers.Authentication.Query.Models;
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
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("ValidateToken")]
        public async Task<IActionResult> CheckValidateToken([FromQuery] AuthuorirzeQueryModel query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
    }
}
