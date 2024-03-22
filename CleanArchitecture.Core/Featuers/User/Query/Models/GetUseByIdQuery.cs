using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.User.Query.Result;
using MediatR;

namespace CleanArchitecture.Core.Featuers.User.Query.Models
{
    public class GetUseByIdQuery(string id) : IRequest<Response<GetUserByIdResponse>>
    {
        public string Id { get; set; } = id;
    }
}
