using CleanArchitecture.Date.Entites.Function;

namespace CleanArchitecture.Services.Abstract
{
    public interface IInstructorServices
    {
        Task<decimal> GetSalarySummation();
        Task<List<GetInstructorDataResult>> GetInstructorDate();
    }
}
