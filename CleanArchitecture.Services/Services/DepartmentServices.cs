using CleanArchitecture.Date.Entites;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Services.Services
{
    public class DepartmentServices(IDapartmentReporesatory dapartmentReporesatory) : IDepartmentServices
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

        public Task<bool> IsDepartmentExist(int id)
        {
            return dapartmentReporesatory.GetTableNoTracking().AnyAsync(x => x.DID.Equals(id));
        }
    }
}
