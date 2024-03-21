using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class InstructorReporesatory(AppDbContext Context) : GenericRepo<Instructor>(Context), IInstructorReporesatory
    {
        private readonly DbSet<Instructor> _instructors = Context.Set<Instructor>();
    }
}
