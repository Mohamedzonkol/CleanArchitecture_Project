using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Authorization.Commands.Models;
using CleanArchitecture.Core.Featuers.Authorization.Querys.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController(IMediator mediator) : AppControllerBase
    {
        [HttpPost("CreateRole")]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut("EditRole")]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            var response = await mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet("GetRoles")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRolesList()
        {
            var response = await mediator.Send(new GetRolesListModels());
            return NewResult(response);
        }
        [HttpGet("GetRole/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleById([FromRoute] string id)
        {
            var response = await mediator.Send(new GetRoleByIdModels(id));
            return NewResult(response);
        }
    }
}
