using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Students.Queries.Models;
using CleanArchitecture.Core.Featuers.Students.Queries.Results;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Date.Entites;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace CleanArchitecture.Core.Featuers.Students.Queries.Handlers
{
    public class Studenthandler(IStudentServices services, IMapper mapper, IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)//Custom response handler
        : ResponseHandler(stringLocalizer), IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
            IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
            IRequestHandler<GetStudentPaginatedQuery, PaginatedResult<GetStudentPaginatedResponse>>
    {
        private readonly IStringLocalizer<SheardResourses.SheardResourses> _stringLocalizer = stringLocalizer;

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await services.GetStudentsAsync();
            var studentListMapper = mapper.Map<List<GetStudentListResponse>>(studentList);
            var result = Success(studentListMapper); //Fun In Response Handler
            result.Meta = new
            {
                Count = studentListMapper.Count()
            };
            return result;
        }
        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await services.GetStudentsAsyncWithIncludeById(request.Id);
            if (student is null)
                return NotFound<GetSingleStudentResponse>(_stringLocalizer[SheardResoursesKeys.NotFound]);//("This Student Is Not Found");
            var studentMapper = mapper.Map<GetSingleStudentResponse>(student);
            return Success(studentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedResponse>> Handle(GetStudentPaginatedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedResponse>> expression = e =>
                new GetStudentPaginatedResponse(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address, e.Department.Localize(e.Department.DNameAr, e.Department.DNameEn));
            // var queryable = services.GetStudentsQueryable();
            var filterQurayble = services.FilterStudentQueryable(request.OrderBy, request.Search);
            var paginatedList = await filterQurayble.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new
            {
                Count = paginatedList.Data.Count()
            };
            return paginatedList;
        }
    }
}
