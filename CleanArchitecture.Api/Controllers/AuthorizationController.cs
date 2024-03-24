using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{

    [ApiController]
    public class AuthorizationController(IMediator mediator) : AppControllerBase
    {
        [HttpPost("CreateRole")]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
