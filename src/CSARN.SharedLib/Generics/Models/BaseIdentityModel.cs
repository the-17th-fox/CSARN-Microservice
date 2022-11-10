using Microsoft.AspNetCore.Identity;

namespace CSARN.SharedLib.Generics.Models
{
    public abstract class BaseIdentityModel : IdentityUser<Guid>, IBaseModelProperties<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
