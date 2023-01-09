using System.Text.Json.Serialization;

namespace Core.ViewModels.Accounts
{
    public class ExtendedAccountViewModel : AccountViewModel
    {
        [JsonPropertyName("RefreshToken.ExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [JsonPropertyName("RefreshToken.IsRevoked")]
        public bool? IsRevoked { get; set; }

        [JsonPropertyName("RefreshToken.IsActive")]
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
