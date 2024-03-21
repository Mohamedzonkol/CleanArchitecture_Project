using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class StudentReporesatory(AppDbContext context) : GenericRepo<Student>(context), IStudentReporesatory
    {
        private readonly DbSet<Student> _students = context.Set<Student>();

        public async Task<List<Student>> GetStudentAsync()
        {
            return await _students.Include(x => x.Department).ToListAsync();
        }
    }
}
