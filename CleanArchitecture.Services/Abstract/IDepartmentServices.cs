using CleanArchitecture.Date.Entites;
using CleanArchitecture.Date.Entites.Procedures;
using CleanArchitecture.Date.Entites.Views;

namespace CleanArchitecture.Services.Abstract
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentAsyncById(int id);
        public Task<List<ViewDepartment>> GetDepartmentStudentView();
        public Task<bool> IsDepartmentExist(int id);
        Task<IReadOnlyList<ViewDepartmentCountProc>> GetDepartmentCountProc(DepartmentCountProcParameter parameter);

    }
}
