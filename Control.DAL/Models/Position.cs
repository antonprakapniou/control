﻿namespace Control.DAL.Models
{
	public sealed class Position
	{
		#region Own properties
		public Guid PositionId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Included { get; set; }
		public string? Addition { get; set; }
		public DateTime? Previous { get; set; }
		public DateTime Next { get; set; }
		public DateTime Created { get; set; }

		#endregion

		#region You never know

		//public double? Minimum { get; set; }
		//public double? Maximum { get; set; }
		//public string? Accuracy { get; set; }

		#endregion

		#region IncludedProperties

		public Guid? MeasuringId { get; set; }
		public Measuring? Measuring { get; set; }
		public Guid? NominationId { get; set; }
		public Nomination? Nomination { get; set; }
		public Guid? OperationId { get; set; }
		public Operation? Operation { get; set; }
		public Guid? OwnerId { get; set; }
		public Owner? Owner { get; set; }
		public Guid? PeriodId { get; set; }
		public Period? Period { get; set; }
		public Guid? StatusId { get; set; }
		public Status? Status { get; set; }
		public Guid? UnitsId { get; set; }
		public Units? Units { get; set; }

		#endregion

		public Position()
		{
			Next=Next
				.AddMonths(Period!.Month)
				.AddDays(-1);

			Created=DateTime.Now;
		}
	}
}
