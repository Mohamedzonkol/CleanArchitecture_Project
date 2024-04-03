using CleanArchitecture.Date.Entites.Function;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Services.Abstract;

namespace CleanArchitecture.Services.Services
{
    public class InstructorServices(AppDbContext context, IInstructorFunctionRepo instructorFunctionRepo) : IInstructorServices
    {
        public async Task<decimal> GetSalarySummation()
        {
            var result = await instructorFunctionRepo.GetSalarySummation("select dbo.GetSumSummantion()");
            return result;
        }
        public async Task<List<GetInstructorDataResult>> GetInstructorDate()
        {
            var result = await instructorFunctionRepo.GetInstructorDate("select * from dbo.GetInstracyureDate() ");
            return result;
        }
    }
}
