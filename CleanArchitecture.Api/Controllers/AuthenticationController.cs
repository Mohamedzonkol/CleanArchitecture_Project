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
        [HttpGet("ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost("SendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmail([FromForm] ResetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromForm] ForgetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
