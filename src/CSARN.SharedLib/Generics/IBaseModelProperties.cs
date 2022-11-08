namespace SharedLib.Generics.Interfaces
{
    internal interface IBaseModelProperties<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
