namespace Control.DAL.Models
{
	public sealed class Measuring
	{
		public Guid MeasuringId { get; set; }
		public string? Name { get; set; }
		public string? Code { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
