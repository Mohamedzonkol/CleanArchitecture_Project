using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Date.Entites.Idetitiy
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}