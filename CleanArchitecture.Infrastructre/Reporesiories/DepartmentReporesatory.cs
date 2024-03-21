using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class DepartmentReporesatory(AppDbContext Context) : GenericRepo<Department>(Context), IDapartmentReporesatory
    {
        private readonly DbSet<Department> _departments = Context.Set<Department>();

    }
}
