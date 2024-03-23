using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Students.Commands.Models;
using CleanArchitecture.Core.Featuers.Students.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StudentController(IMediator mediator) : AppControllerBase
    {
        [HttpGet("StudentList")]
        public async Task<IActionResult> GetStudents()
        {
            var response = await mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        [HttpGet("paginatedList")]
        [AllowAnonymous]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(response);
        }
        [HttpPost("Student/Create")]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut("Student/Update")]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("StudentDelete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
