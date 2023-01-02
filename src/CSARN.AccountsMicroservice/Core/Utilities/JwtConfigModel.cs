using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities
{
    public class JwtConfigModel
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int LifetimeHours { get; set; }

        public SymmetricSecurityKey? Key { get; set; }
    }
}
