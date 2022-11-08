using Microsoft.AspNetCore.Identity;
using SharedLib.Generics.Interfaces;

namespace SharedLib.Generics
{
    public abstract class BaseIdentityModel : IdentityUser<Guid>, IBaseModelProperties<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
