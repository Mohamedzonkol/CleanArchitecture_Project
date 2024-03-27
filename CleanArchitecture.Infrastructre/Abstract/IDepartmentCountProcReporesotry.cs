using CleanArchitecture.Date.Entites.Procedures;

namespace CleanArchitecture.Infrastructre.Abstract
{
    public interface IDepartmentCountProcReporesotry
    {
        Task<IReadOnlyList<ViewDepartmentCountProc>> GetDepartmentCountProc(DepartmentCountProcParameter parameter);
    }
}
