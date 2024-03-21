using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Models;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Date.Entites;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace CleanArchitecture.Core.Featuers.Department.Query.Handlers
{
    public class DepartmentHandlers(IMapper mapper, IDepartmentServices services,
        IStudentServices studentServices, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer), IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //service Get By Id include St sub ins
            var response = await services.GetDepartmentAsyncById(request.Id);
            //check Is Not exist
            if (response == null) return NotFound<GetDepartmentByIdResponse>(stringLocalizer[SheardResoursesKeys.NotFound]);
            //mapping 
            var _mapper = mapper.Map<GetDepartmentByIdResponse>(response);

            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = studentServices.GetStudentsByDepartmentQueryable(request.Id);
            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            _mapper.StudentList = PaginatedList;

            // Log.Information($"Get Department By Id {request.Id}!");
            //return response
            return Success(_mapper);
        }
    }
}
