namespace Control.DAL.Models
{
	public sealed class Position
	{
        private DateTime _nextDate;
        private StatusEnum _status;

		#region Own properties

		public Guid PositionId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Included { get; set; }
		public string? Addition { get; set; }
		public DateTime PreviousDate { get; set; }
		public DateTime NextDate
		{
			get
			{
				if (Period!=null) return _nextDate=PreviousDate.AddMonths(Period!.Month).AddDays(-1);
				else return default;
						
			}
			set=>_nextDate=value; 
		}
		public DateTime Created { get; set; }
        public StatusEnum Status
        {
            get => _status=SetStatus(NextDate);
			private set => _status=value;
        }

        #endregion

        #region IncludedPropertiesId

        public Guid? MeasuringId { get; set; }
        public Guid? NominationId { get; set; }
        public Guid? OperationId { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? PeriodId { get; set; }
        public Guid? CategoryId { get; set; }

        #endregion

        #region IncludedProperties

        public Measuring? Measuring { get; set; }
		public Nomination? Nomination { get; set; }
		public Operation? Operation { get; set; }
		public Owner? Owner { get; set; }
		public Period? Period { get; set; }
		public Category? Category { get; set; }

        #endregion

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
