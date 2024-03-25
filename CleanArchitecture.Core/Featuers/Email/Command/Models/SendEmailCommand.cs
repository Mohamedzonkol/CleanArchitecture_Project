using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.Email.Command.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
