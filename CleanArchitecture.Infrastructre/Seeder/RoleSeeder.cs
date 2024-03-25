using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {

            var roleCount = await roleManager.Roles.CountAsync();
            if (roleCount <= 0)
            {

                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                });
            }
        }
    }

}
