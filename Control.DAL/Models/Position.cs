namespace Control.DAL.Models
{
	public sealed class Position
	{
		private StatusEnum _status;

		#region Own properties
		public Guid PositionId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Included { get; set; }
		public string? Addition { get; set; }
		public DateTime? PreviousDate { get; set; }
		public DateTime NextDate { get; set; }
		public DateTime Created { get; set; }
        public StatusEnum Status
        {
            get => _status=SetStatus(NextDate);
			set => _status=value;
        }

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

		#endregion

		public Position()
		{
			NextDate=NextDate
				.AddMonths(Period!.Month)
				.AddDays(-1);

			Created=DateTime.Now;
		}

		private static StatusEnum SetStatus(DateTime nextDate)
		{
			DateTime currentDate= DateTime.Now;

			if (currentDate>nextDate) return StatusEnum.Invalid;
			else if (nextDate.Month-currentDate.Month==1) return StatusEnum.NextMonthControl;
            else if (nextDate.Month-currentDate.Month==0) return StatusEnum.CurrentMonthControl;
			else return StatusEnum.Valid;
        }
	}
}
