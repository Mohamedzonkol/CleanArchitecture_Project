using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Generics.Abstract;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using CleanArchitecture.Infrastructre.Reporesiories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructre
{
    public static class ModelInfrastructreDependencies
    {
        public static IServiceCollection AddInfrastructreDependencies(this IServiceCollection service)
        {
            service.AddTransient<IStudentReporesatory, StudentReporesatory>();
            service.AddTransient<IDapartmentReporesatory, DepartmentReporesatory>();
            service.AddTransient<ISubjectReporesatory, SubjectReporesatory>();
            service.AddTransient<IInstructorReporesatory, InstructorReporesatory>();
            service.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            return service;
        }
    }
}
