using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authentication.Query.Models
{
    public class AuthuorirzeQueryModel : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
