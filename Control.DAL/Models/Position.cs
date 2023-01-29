namespace Control.DAL.Models
{
	public sealed class Position:BaseModel
	{
		#region Own properties

		public string? DeviceType { get; set; }
        public string? FactoryNumber { get; set; }
        public string? Description { get; set; }
		public string? Included { get; set; }
		public string? Addition { get; set; }
		public DateTime PreviousDate { get; set; }
		public DateTime NextDate { get; set; }
        public DateTime Created { get; set; }
        public StatusEnum Status { get; set; }
		public string? Picture { get; set; }

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
	}
}
