namespace Control.DAL.Models
{
    public sealed class Category:BaseModel
    {
        public string? Name { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }
}
