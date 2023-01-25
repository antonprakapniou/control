using Control.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class PositionVM
	{
		#region Own properties

		public Guid Id { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }

		[DisplayName("Description")]
		public string? Description { get; set; }

		[DisplayName("Included")]
		public string? Included { get; set; }

		[DisplayName("Addition")]
		public string? Addition { get; set; }

		[Required]
		[DisplayName("Previous")]
		public DateTime? PreviousDate { get; set; }

		[DisplayName("Next")]
		public DateTime NextDate { get; set; }

		[DisplayName("Created")]
		public DateTime Created { get; set; }

		[DisplayName("Status")]
		public StatusEnum Status { get; set; }

        [DisplayName("Picture")]
        public string? Picture { get; set; }

        #endregion

        #region IncludedPropertiesId

        [DisplayName("Measuring")]
        public Guid? MeasuringId { get; set; }

        [DisplayName("Nomination")]
        public Guid? NominationId { get; set; }

        [DisplayName("Operation")]
        public Guid? OperationId { get; set; }

        [DisplayName("Owner")]
        public Guid? OwnerId { get; set; }

        [Required]
        [DisplayName("Period")]
        public Guid? PeriodId { get; set; }

        [DisplayName("CategoryId")]
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
