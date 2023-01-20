using System.ComponentModel.DataAnnotations.Schema;

namespace Control.DAL.Models
{
	public sealed class Position
	{
		public Guid PositionId { get; set; }
		public string? Name { get; set; }
		public double Minimum { get; set; }
		public double Maximum { get; set; }
		public string? Accuracy { get; set; }
		public string? Included { get; set; }
		public string? Addition { get; set; }
		public DateTime? Previous { get; set; }
		public DateTime Next { get; set; }
		public DateTime? Created { get; set; }

		#region IncludedProperties
		[ForeignKey("MeasuringId")]public Measuring? Measuring { get; set; }
		[ForeignKey("NominationId")] public Nomination? Nomination { get; set; }
		[ForeignKey("OperationId")] public Operation? Operation { get; set; }
		[ForeignKey("OwnerId")] public Owner? Owner { get; set; }
		[ForeignKey("PeriodId")] public Period? Period { get; set; }
		[ForeignKey("StatusId")] public Status? Status { get; set; }
		[ForeignKey("UnitsId")] public Units? Units { get; set; }

		#endregion

		public Position()
		{
			Next=Next.AddMonths(Period!.Month);
			Created= DateTime.UtcNow;
		}

	}
}
