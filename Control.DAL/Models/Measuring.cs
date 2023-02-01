namespace Control.DAL.Models
{
	public sealed class Measuring:BaseModel
	{
		public string? Name { get; set; }
		public string? Code { get; set; }
        public string? FullName { get => $"{Code} {Name}"; set { } }
        public ICollection<Position>? Positions { get; set; }
	}
}
