namespace Control.DAL.Models
{
	public sealed class Units
	{
		public Guid UnitsId { get; set; }
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }

	}
}
