using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.AccountsMsvc.Models
{
    [Index(nameof(AccountId), IsUnique = true)]
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public bool IsRevoked { get; set; } = false;

        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        [NotMapped]
        public bool IsActive => !(IsExpired || IsRevoked);

        public RefreshToken(DateTime expiresAt, Guid accountId)
        {
            ExpiresAt = expiresAt;
            AccountId = accountId;
        }
    }
}
