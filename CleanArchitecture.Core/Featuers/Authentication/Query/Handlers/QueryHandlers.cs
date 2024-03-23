﻿using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.Authentication.Query.Models;
using CleanArchitecture.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.Authentication.Query.Handlers
{
    public class QueryHandlers(IAuthenticationServices authenticationServices,
        IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<AuthuorirzeQueryModel, Response<string>>
    {
        public async Task<Response<string>> Handle(AuthuorirzeQueryModel request, CancellationToken cancellationToken)
        {
            var accessToken = await authenticationServices.ValidateToken(request.AccessToken);
            if (accessToken == "Not Expired") return Success("This  Token Is Still Valid");
            return BadRequest<string>("This access Token Is Expired");
        }
    }
}