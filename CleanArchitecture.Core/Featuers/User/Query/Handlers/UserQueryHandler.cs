using AutoMapper;
using CleanArchitecture.Core.Bases;
using CleanArchitecture.Core.Featuers.User.Query.Models;
using CleanArchitecture.Core.Featuers.User.Query.Result;
using CleanArchitecture.Core.SheardResourses;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Date.Entites.Idetitiy;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Featuers.User.Query.Handlers
{
    public class UserQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager
        , IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
        : ResponseHandler(stringLocalizer),
            IRequestHandler<GetUserListQuery, PaginatedResult<GetUserListResponse>>,
            IRequestHandler<GetUseByIdQuery, Response<GetUserByIdResponse>>
    {
        public async Task<PaginatedResult<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = userManager.Users.AsQueryable();
            var paginatedList =
                await mapper.ProjectTo<GetUserListResponse>(users)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


        public async Task<Response<GetUserByIdResponse>> Handle(GetUseByIdQuery request, CancellationToken cancellationToken)
        {
            // var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            var user = await userManager.FindByIdAsync(request.Id);
            if (user is null) return NotFound<GetUserByIdResponse>(stringLocalizer[SheardResoursesKeys.NotFound]);
            var userMapping = mapper.Map<GetUserByIdResponse>(user);
            return Success(userMapping);
        }
    }
}
