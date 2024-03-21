using CleanArchitecture.Date.Entites;

namespace CleanArchitecture.Services.Abstract
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentAsyncById(int id);
        public Task<bool> IsDepartmentExist(int id);

    }
}
