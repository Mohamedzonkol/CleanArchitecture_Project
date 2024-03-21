using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Infrastructre.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructre
{
    public static class ServiceResgistration
    {
        public static IServiceCollection AddResgistraionDependencies(this IServiceCollection service)
        {
            service.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            return service;
        }
    }
}
