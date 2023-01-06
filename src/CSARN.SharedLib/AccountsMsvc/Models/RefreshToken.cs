using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.AccountsMsvc.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Revoked { get; set; } = false;

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow > ExpiresAt;
        [NotMapped]
        public bool IsActive => !(IsExpired || Revoked);

        public Account Account { get; set; } = null!;

        public RefreshToken(DateTime expiresAt)
        {
            ExpiresAt = expiresAt;
            //CreatedAt = DateTime.UtcNow;
        }
    }
}
