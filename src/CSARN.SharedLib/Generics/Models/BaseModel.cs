namespace SharedLib.Generics.Models
{
    public abstract class BaseModel : IBaseModelProperties<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
