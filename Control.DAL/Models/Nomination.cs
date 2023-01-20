namespace Control.DAL.Models
{
	public sealed class Nomination
	{
		public Guid NominationId { get; set; }
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
