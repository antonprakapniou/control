using Control.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class PositionVM
	{
		#region Own properties
		public Guid PositionId { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }

		[DisplayName("Description")]
		public string? Description { get; set; }

		[DisplayName("Included")]
		public string? Included { get; set; }

		[DisplayName("Addition")]
		public string? Addition { get; set; }

		[DisplayName("Previous")]
		public DateTime? PreviousDate { get; set; }

		[DisplayName("Next")]
		public DateTime NextDate { get; set; }

		[DisplayName("Created")]
		public DateTime Created { get; set; }

		[DisplayName("Status")]
		public StatusEnum Status { get; set; }

        #endregion

        #region You never know

        //public double? Minimum { get; set; }
        //public double? Maximum { get; set; }
        //public string? Accuracy { get; set; }

        #endregion

        #region IncludedProperties

        [DisplayName("Measuring")]
		//public Guid? MeasuringId { get; set; }
		public Measuring? Measuring { get; set; }

		[DisplayName("Nomination")]
		//public Guid? NominationId { get; set; }
		public Nomination? Nomination { get; set; }

		[DisplayName("Operation")]
		//public Guid? OperationId { get; set; }
		public Operation? Operation { get; set; }

		[DisplayName("Owner")]
		//public Guid? OwnerId { get; set; }
		public Owner? Owner { get; set; }

		[DisplayName("Period")]
		//public Guid? PeriodId { get; set; }
		public Period? Period { get; set; }
        public Category? Category { get; set; }

        #endregion
    }
}
