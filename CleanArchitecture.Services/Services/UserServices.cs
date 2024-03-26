using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Services.Services
{
    public class UserServices(UserManager<ApplicationUser> userManager,
        AppDbContext context, IUrlHelper urlHelper, IEmailServices emailServices, IHttpContextAccessor httpContextAccessor)
        : IUserServices
    {

        public async Task<string> AddUserAsync(ApplicationUser appUser, string password)
        {
            var trans = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var user = await userManager.FindByEmailAsync(appUser.Email);
                if (user is not null) return "EmailIsExist";
                var userName = await userManager.FindByNameAsync(appUser.UserName);
                if (userName is not null) return "UserNameIsExist";
                var UserCreated = await userManager.CreateAsync(appUser, password).ConfigureAwait(false);
                if (!UserCreated.Succeeded)
                    return string.Join(",", UserCreated.Errors.Select(x => x.Description).ToList());
                await userManager.AddToRoleAsync(appUser, "User").ConfigureAwait(false);
                //Send Confirm Email
                var code = await userManager.GenerateEmailConfirmationTokenAsync(appUser).ConfigureAwait(false);
                var httpAccessor = httpContextAccessor.HttpContext.Request;
                var returnUrl = $"{httpAccessor.Scheme}://{httpAccessor.Host}{urlHelper.Action("ConfirmEmail", "Authentication", new { userId = appUser.Id, code = code })}";
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //message or body
                await emailServices.SendEmail(appUser.Email, message, "ConFirm Email").ConfigureAwait(false);

                await trans.CommitAsync().ConfigureAwait(false);
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                return "Failed";
            }
        }
    }
}
