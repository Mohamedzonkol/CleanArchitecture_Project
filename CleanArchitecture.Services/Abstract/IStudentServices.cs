using CleanArchitecture.Date.Entites;
using CleanArchitecture.Date.Helpers;

namespace CleanArchitecture.Services.Abstract
{
    public interface IStudentServices
    {
        public Task<List<Student>> GetStudentsAsync();
        public Task<Student?> GetStudentsAsyncWithIncludeById(int id);
        public Task<Student?> GetStudentsAsyncById(int id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);
        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);
        public IQueryable<Student> GetStudentsQueryable();
        public IQueryable<Student> GetStudentsByDepartmentQueryable(int DID);
        public IQueryable<Student> FilterStudentQueryable(StudentOrderingEnum orderingEnum, string search);

    }
}
