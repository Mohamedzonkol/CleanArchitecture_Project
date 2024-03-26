using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Date.Entites.Idetitiy
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Code { get; set; }
        [InverseProperty("User")]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}