using CleanArchitecture.Services.Abstract;
using CleanArchitecture.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Services
{
    public static class ModelServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection service)
        {
            service.AddTransient<IStudentServices, StudentServices>();
            service.AddTransient<IDepartmentServices, DepartmentServices>();
            service.AddTransient<IAuthenticationServices, AuthenticationServices>();
            service.AddTransient<IAuthorizationServices, AuthorizationServices>();
            return service;
        }

    }
}
