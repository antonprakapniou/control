namespace Control.DAL.Models
{
	public sealed class Operation:BaseModel
	{
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
