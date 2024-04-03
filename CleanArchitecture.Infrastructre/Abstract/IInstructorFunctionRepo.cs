using CleanArchitecture.Date.Entites.Function;

namespace CleanArchitecture.Infrastructre.Abstract
{
    public interface IInstructorFunctionRepo
    {
        Task<decimal> GetSalarySummation(string query);
        Task<List<GetInstructorDataResult>> GetInstructorDate(string query);
    }
}
