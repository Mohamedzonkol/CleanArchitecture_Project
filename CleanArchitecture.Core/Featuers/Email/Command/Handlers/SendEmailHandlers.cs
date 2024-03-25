using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Email.Command.Models;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Email.Command.Handlers
{
    public class SendEmailHandlers(IEmailServices emailServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<SendEmailCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await emailServices.SendEmail(request.Email, request.Message, "For Work");
            return Success(response);
        }
    }
}
