namespace Control.DAL.Models
{
	public sealed class Period:BaseModel
	{
		public string? Name { get; set; }
        public int Month { get; set; }

        public ICollection<Position>? Positions { get; set; }
	}
}
