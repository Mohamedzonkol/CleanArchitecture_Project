using CleanArchitecture.Core.Bases;
using MediatR;

namespace CleanArchitecture.Core.Featuers.User.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string CuurentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
