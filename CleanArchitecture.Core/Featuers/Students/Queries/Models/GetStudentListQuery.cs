using CleanArchitecture.Date;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Students.Queries.Results;

namespace CleanArchitecture.Core.Featuers.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>> //Custom response
    {
    }
}
