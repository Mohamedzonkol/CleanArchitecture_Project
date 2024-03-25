using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Email.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public class EmailController(IMediator mediator) : AppControllerBase
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
