namespace Control.DAL.Models
{
	public sealed class Owner
	{
		public Guid Id { get; set; }
		public string? Shop { get; set; }
		public string? Production { get; set; }
		public string? Master { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
