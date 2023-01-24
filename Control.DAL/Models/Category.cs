﻿namespace Control.DAL.Models
{
    public sealed class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }
}
