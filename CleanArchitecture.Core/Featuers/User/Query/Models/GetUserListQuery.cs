using CleanArchitecture.Core.Featuers.User.Query.Result;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Featuers.User.Query.Models
{
    public class GetUserListQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
