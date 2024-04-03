using CleanArchitecture.Api.Bases;
using CleanArchitecture.Core.Featuers.Instructor.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    public class InstructorController(IMediator mediator) : AppControllerBase
    {
        [HttpGet("SummationSalary")]
        public async Task<IActionResult> SummationSalaryOfInstructor()
        {
            var response = await mediator.Send(new GetSummationSalaryQuery());
            return NewResult(response);
        }
        [HttpGet("GetInstructorDate")]
        public async Task<IActionResult> GetInstructorDate()
        {
            var response = await mediator.Send(new GetInstructorDateQuery());
            return NewResult(response);
        }
    }
}
