namespace Control.DAL.Models
{
	public sealed class Status
	{
		public Guid StatusId { get; set; }
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }

	}
}
