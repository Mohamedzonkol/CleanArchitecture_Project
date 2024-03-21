using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Students.Commands.Models;
using CleanArchitecture.Date.Entites;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Students.Commands.Handlers
{
    public class StudentCommandHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SheardResourses.SheardResourses> localizer) : ResponseHandler(localizer),
        IRequestHandler<AddStudentCommand, Response<string>>
        , IRequestHandler<EditStudentCommand, Response<string>>
        , IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer = localizer;

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = mapper.Map<Student>(request);
            var result = await studentServices.AddAsync(studentMapper);
            //if (result == "Exist") return UnprocessableEntity<string>("Student Is Exist");
            if (result == "Success") return Created("Added Is Success");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentServices.GetStudentsAsyncById(request.Id);
            if (student == null) return NotFound<string>("This Student Is Not found");
            //   var studentMapper = mapper.Map<Student>(request);
            var studentMapper = mapper.Map(request, student);
            var result = await studentServices.EditAsync(studentMapper);
            if (result == "Success") return Success($"Edit Is Success For {studentMapper.StudID}");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentServices.GetStudentsAsyncById(request.Id);
            if (student == null) return NotFound<string>("This Student Is Not found");
            var result = await studentServices.DeleteAsync(student);
            if (result == "Success") return Deleted<string>($"Delete Is Successfully For {request.Id}");
            return BadRequest<string>();
        }
    }
}
