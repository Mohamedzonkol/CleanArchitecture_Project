using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Infrastructre.Generics.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Reporesiories
{
    public class RefreshTokenReporesatory(AppDbContext Context) : GenericRepo<UserRefreshToken>(Context), IRefreshTokenReporesatory
    {
        private readonly DbSet<UserRefreshToken> _userRefreshTokens = Context.Set<UserRefreshToken>();

    }
}
