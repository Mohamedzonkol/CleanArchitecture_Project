using CleanArchitecture.Date.Entites;
using CleanArchitecture.Date.Entites.Procedures;
using CleanArchitecture.Date.Entites.Views;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Services.Services
{
    public class DepartmentServices(IDapartmentReporesatory dapartmentReporesatory,
        IDepartmentCountProcReporesotry procReporesotry, IViewReporesatory<ViewDepartment> viewReporesatory) : IDepartmentServices
    {
        public async Task<Department> GetDepartmentAsyncById(int id)
        {
            var department = dapartmentReporesatory.GetTableNoTracking().Where(x => x.DID == id)
                .Include(x => x.DepartmentSubjects)
                .Include(x => x.Students)
                .Include(x => x.Instructor)
               .SingleOrDefault();

            return department;
        }

        public async Task<List<ViewDepartment>> GetDepartmentStudentView()
        {
            return await viewReporesatory.GetTableNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public Task<bool> IsDepartmentExist(int id)
        {
            return dapartmentReporesatory.GetTableNoTracking().AnyAsync(x => x.DID.Equals(id));
        }

        public async Task<IReadOnlyList<ViewDepartmentCountProc>> GetDepartmentCountProc(DepartmentCountProcParameter parameter)
        {
            return await procReporesotry.GetDepartmentCountProc(parameter).ConfigureAwait(false);
        }
    }
}
