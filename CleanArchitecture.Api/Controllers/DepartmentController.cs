using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public class DepartmentController(IMediator mediator) : AppControllerBase
    {
        [HttpGet("DepartmentById")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpGet("DepartmentStudentView")]
        [SwaggerOperation(summary: "Dealing With Views", OperationId = "DepartmentStudentView")]

        public async Task<IActionResult> DepartmentStudentView()
        {
            var response = await mediator.Send(new GetDepartmentStudentQuery());
            return NewResult(response);
        }
        [HttpGet("DepartmentStudentProc/{id}")]
        [SwaggerOperation(summary: "Dealing With Procedures", OperationId = "DepartmentStudentProc")]

        public async Task<IActionResult> DepartmentStudentProc([FromRoute] int id)
        {
            var response = await mediator.Send(new GetDepartmentStudentProcQuery(id));
            return NewResult(response);
        }
    }
}
