using CleanArchitecture.Date.Entites.Procedures;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using StoredProcedureEFCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class DepartmentCountProcReporesotry(AppDbContext context) : IDepartmentCountProcReporesotry
    {
        public async Task<IReadOnlyList<ViewDepartmentCountProc>> GetDepartmentCountProc(DepartmentCountProcParameter parameter)
        {
            var rows = new List<ViewDepartmentCountProc>();
            await context.LoadStoredProc(nameof(ViewDepartmentCountProc))
                .AddParam(nameof(DepartmentCountProcParameter.DID), parameter.DID)
                .ExecAsync(async r => rows = await r.ToListAsync<ViewDepartmentCountProc>());
            return rows;
        }
    }
}
