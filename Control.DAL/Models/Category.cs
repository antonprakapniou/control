namespace Control.DAL.Models
{
    public sealed class Category
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }
}
