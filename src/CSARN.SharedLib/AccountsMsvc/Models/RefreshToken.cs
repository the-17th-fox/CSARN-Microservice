using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.AccountsMsvc.Models
{
    [Owned]
    public class RefreshToken
    {
        [Column("RefrToken")]
        public Guid Token { get; set; } = Guid.NewGuid();

        [Column("RefrToken.ExpAt")]
        public DateTime ExpiresAt { get; set; }

        [Column("RefrToken.IsRevoked")]
        public bool IsRevoked { get; set; } = false;

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        [NotMapped]
        public bool IsActive => !(IsExpired || IsRevoked);

        public RefreshToken(DateTime expiresAt)
        {
            ExpiresAt = expiresAt;
        }
    }
}
