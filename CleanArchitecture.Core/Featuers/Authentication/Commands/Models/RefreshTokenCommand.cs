using CleanArchitecture.Date.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Bases;

namespace CleanArchitecture.Core.Featuers.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
