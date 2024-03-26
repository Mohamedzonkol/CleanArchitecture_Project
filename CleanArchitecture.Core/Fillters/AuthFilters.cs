using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Services.AuthServices.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.Core.Fillters
{
    public class AuthFilters(ICurrentUserServices currentUserServices, UserManager<ApplicationUser> userManager) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var roles = await currentUserServices.GetUserRoles();
                if (roles.All(x => x != "User"))
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = 403
                    };
                else await next();




            }

        }
    }
}
