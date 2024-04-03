using CleanArchitecture.Date.Entites.Function;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System.Data;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class InstructorFunctionRepo(AppDbContext context) : IInstructorFunctionRepo
    {
        public async Task<decimal> GetSalarySummation(string query)
        {
            await using var cmd = context.Database.GetDbConnection().CreateCommand();
            if (cmd.Connection!.State != ConnectionState.Open)
                await cmd.Connection.OpenAsync().ConfigureAwait(false);
            cmd.CommandText = query;
            var value = await cmd.ExecuteScalarAsync();
            await cmd.Connection.CloseAsync();
            return decimal.Parse(value.ToString() ?? "0");
        }

        public async Task<List<GetInstructorDataResult>> GetInstructorDate(string query)
        {
            await using var cmd = context.Database.GetDbConnection().CreateCommand();
            if (cmd.Connection!.State != ConnectionState.Open)
                await cmd.Connection.OpenAsync().ConfigureAwait(false);
            cmd.CommandText = query;
            var reader = await cmd.ExecuteReaderAsync();
            var value = await reader.ToListAsync<GetInstructorDataResult>();
            await cmd.Connection.CloseAsync();
            return value;
        }
    }
}
