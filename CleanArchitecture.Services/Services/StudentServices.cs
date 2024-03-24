using CleanArchitecture.Date.Entites;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Services.Services
{
    public class StudentServices(IStudentReporesatory studentReporesatory) : IStudentServices
    {
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await studentReporesatory.GetStudentAsync();
        }

        public async Task<Student?> GetStudentsAsyncWithIncludeById(int id)
        {
            // var student= await _studentReporesatory.GetByIdAsync(id);
            var student = studentReporesatory
                .GetTableNoTracking().
                Include(x => x.Department).FirstOrDefault(x => x.StudID == id);
            return student;
        }

        public async Task<Student?> GetStudentsAsyncById(int id)
        {
            var student = await studentReporesatory.GetByIdAsync(id);
            return student;
        }

        public async Task<string> AddAsync(Student newStudent)
        {
            var student = studentReporesatory
                .GetTableNoTracking().
                FirstOrDefault(x => x.NameAr.Equals(newStudent.NameAr));
            if (student is not null)
                return "Exist";
            await studentReporesatory.AddAsync(newStudent);
            return "Success";

        }

        public async Task<string> EditAsync(Student student)
        {
            await studentReporesatory.UpdateAsync(student).ConfigureAwait(false);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trands = studentReporesatory.BeginTransaction();
            try
            {
                await studentReporesatory.DeleteAsync(student).ConfigureAwait(false);
                await trands.CommitAsync().ConfigureAwait(false);
                return "Success";
            }
            catch (Exception e)
            {
                await trands.RollbackAsync();
                return "Failed";
            }

        }

        public Task<bool> IsNameArExist(string name)
        {
            var student = studentReporesatory.GetTableNoTracking().FirstOrDefault(x => x.NameAr.Equals(name));
            if (student is null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }
        public Task<bool> IsNameEnExist(string name)
        {
            var student = studentReporesatory.GetTableNoTracking().FirstOrDefault(x => x.NameEn.Equals(name));
            if (student is null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var student = await studentReporesatory.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.NameEn.Equals(name) & !x.StudID.Equals(id));
            if (student is null)
                return false;
            return true;
        }
        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var student = await studentReporesatory.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.NameAr.Equals(name) & !x.StudID.Equals(id));
            if (student is null)
                return false;
            return true;
        }

        public IQueryable<Student> GetStudentsQueryable()
        {
            return studentReporesatory.GetTableNoTracking().Include(x => x.Department)
                .AsQueryable();
        }

        public IQueryable<Student> GetStudentsByDepartmentQueryable(int DID)
        {
            return studentReporesatory.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }

        public IQueryable<Student> FilterStudentQueryable(StudentOrderingEnum ordering, string search)
        {
            var queryable = studentReporesatory.GetTableNoTracking().Include(x => x.Department)
                .AsQueryable();
            if (search is not null)
                queryable = queryable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            switch (ordering)
            {
                case StudentOrderingEnum.StudID:
                    queryable = queryable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    queryable = queryable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    queryable = queryable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.Department:
                    queryable = queryable.OrderBy(x => x.Department.DNameAr);
                    break;
            }
            return queryable;
        }
    }
}
