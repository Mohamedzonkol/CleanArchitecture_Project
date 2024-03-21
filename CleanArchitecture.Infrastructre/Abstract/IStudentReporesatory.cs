using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Generics.Abstract;

namespace CleanArchitecture.Infrastructre.Abstract
{
    public interface IStudentReporesatory : IGenericRepo<Student>
    {
        public Task<List<Student>> GetStudentAsync();
    }
}
