using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.ViewModels.Accounts
{
    public class ExtendedAccountViewModel : AccountViewModel
    {
        [JsonPropertyName("RefreshTokenExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [JsonPropertyName("IsRefreshTokenRevoked")]
        public bool? IsRevoked { get; set; }

        [JsonPropertyName("IsRefreshTokenActive")]
        public bool IsActive
        {
            get
            {
                if(ExpiresAt == null || IsRevoked == null) return false;
                bool isExpired = DateTime.UtcNow >= ExpiresAt;
                bool isRevoked = IsRevoked ?? false;
                return !(isExpired || isRevoked);
            }
        }
    }
}
