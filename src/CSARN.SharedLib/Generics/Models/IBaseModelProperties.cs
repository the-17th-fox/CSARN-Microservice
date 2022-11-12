﻿namespace SharedLib.Generics.Models
{
    public interface IBaseModelProperties<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
