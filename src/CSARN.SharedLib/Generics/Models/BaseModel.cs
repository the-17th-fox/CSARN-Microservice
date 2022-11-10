namespace CSARN.SharedLib.Generics.Models
{
    public abstract class BaseModel : IBaseModelProperties<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
