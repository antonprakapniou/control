namespace Control.DAL.Models
{
	public sealed class Period
	{
		private const int _defaultPeriod = 12;
		private int _month;
		public Guid PeriodId { get; set; }
		public string? Name { get; set; }
		public ICollection<Position>? Positions { get; set; }

		public int Month 
		{
			get
			{
				if (int.TryParse(Name!, out _month)) return _month;
				else return _defaultPeriod;				
			}

			private set
			{
				_month= value;
			}
		}
	}
}
