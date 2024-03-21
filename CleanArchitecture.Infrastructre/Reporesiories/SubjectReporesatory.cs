using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class SubjectReporesatory(AppDbContext Context) : GenericRepo<Subjects>(Context), ISubjectReporesatory
    {
        private readonly DbSet<Subjects> _subjects = Context.Set<Subjects>();
    }
}
