using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Date.Entites.Idetitiy
{
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        //  [ForeignKey(nameof(User))] صح بردو
        public string UserId { get; set; }
        public string? JwtId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserRefreshTokens")]
        public virtual ApplicationUser? User { get; set; }
    }
}
