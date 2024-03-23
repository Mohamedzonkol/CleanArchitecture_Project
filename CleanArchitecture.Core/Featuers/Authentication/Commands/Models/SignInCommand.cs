using CleanArchitecture.Core.Bases;
using CleanArchitecture.Date.Helpers;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
