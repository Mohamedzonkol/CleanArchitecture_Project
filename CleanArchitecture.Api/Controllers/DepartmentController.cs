using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IMediator mediator) : AppControllerBase
    {
        [HttpGet("DepartmentById")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
    }
}
