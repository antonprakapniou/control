namespace Control.DAL.Models
{
	public sealed class Operation
	{
		public Guid OperationId { get; set; }
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
