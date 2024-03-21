using CleanArchitecture.Core.Beheviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Core
{
    public static class ModelCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection service)
        {
            service.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidtorBehevior<,>));

            return service;
        }
    }
}
