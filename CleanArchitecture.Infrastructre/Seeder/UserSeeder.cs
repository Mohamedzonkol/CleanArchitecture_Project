using CleanArchitecture.Date.Entites.Idetitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructre.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var userCount = await userManager.Users.CountAsync();
            if (userCount <= 0)
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = "MohamedZonkol",
                    FullName = "Mohamed Zonkol",
                    Email = "mohamed.zonkol@gmail.com",
                    Country = "Egypt",
                    PhoneNumber = "01029054588",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await userManager.CreateAsync(defaultUser, "Mo@123");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
