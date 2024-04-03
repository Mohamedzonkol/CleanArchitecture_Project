using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Instructor.Query.Models;
using CleanArchitecture.Date.Entites.Function;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Instructor.Query.Handlers
{
    public class InstructorHandler(IMapper mapper, IInstructorServices instructorServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<GetSummationSalaryQuery, Response<decimal>>,
            IRequestHandler<GetInstructorDateQuery, Response<List<GetInstructorDataResult>>>
    {
        public async Task<Response<decimal>> Handle(GetSummationSalaryQuery request, CancellationToken cancellationToken)
        {
            var result = await instructorServices.GetSalarySummation();
            return Success(result);
        }

        public async Task<Response<List<GetInstructorDataResult>>> Handle(GetInstructorDateQuery request, CancellationToken cancellationToken)
        {
            var result = await instructorServices.GetInstructorDate();
            return Success(result);
        }
    }
}
