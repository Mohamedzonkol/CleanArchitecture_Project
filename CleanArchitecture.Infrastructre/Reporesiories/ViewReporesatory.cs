using CleanArchitecture.Date.Entites.Views;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class ViewReporesatory(AppDbContext Context) : GenericRepo<ViewDepartment>(Context), IViewReporesatory<ViewDepartment>
    {
        private readonly DbSet<ViewDepartment> _departmentView = Context.Set<ViewDepartment>();

    }
}
