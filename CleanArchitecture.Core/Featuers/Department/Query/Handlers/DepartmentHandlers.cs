using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Department.Query.Models;
using CleanArchitecture.Core.Featuers.Department.Query.Result;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Date.Entites;
using CleanArchitecture.Date.Entites.Procedures;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace CleanArchitecture.Core.Featuers.Department.Query.Handlers
{
    public class DepartmentHandlers(IMapper mapper, IDepartmentServices services,
        IStudentServices studentServices, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
            IRequestHandler<GetDepartmentStudentQuery, Response<List<GetDepartmentStudentResult>>>,
            IRequestHandler<GetDepartmentStudentProcQuery, Response<GetDepartmentStudentProcResult>>

    {

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await services.GetDepartmentAsyncById(request.Id);
            if (response == null) return NotFound<GetDepartmentByIdResponse>(stringLocalizer[SheardResoursesKeys.NotFound]);
            //mapping 
            var _mapper = mapper.Map<GetDepartmentByIdResponse>(response);
            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = studentServices.GetStudentsByDepartmentQueryable(request.Id);
            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            _mapper.StudentList = PaginatedList;
            // Log.Information($"Get Department By Id {request.Id}!");
            return Success(_mapper);
        }

        public async Task<Response<List<GetDepartmentStudentResult>>> Handle(GetDepartmentStudentQuery request, CancellationToken cancellationToken)
        {
            var result = await services.GetDepartmentStudentView();
            var resultMapper = mapper.Map<List<GetDepartmentStudentResult>>(result);
            return Success(resultMapper);

        }

        public async Task<Response<GetDepartmentStudentProcResult>> Handle(GetDepartmentStudentProcQuery request, CancellationToken cancellationToken)
        {
            var parameter = mapper.Map<DepartmentCountProcParameter>(request);
            var result = await services.GetDepartmentCountProc(parameter);
            var resultMapper = mapper.Map<GetDepartmentStudentProcResult>(result.FirstOrDefault());
            return Success(resultMapper);
        }
    }
}
