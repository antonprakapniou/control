using Control.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class PositionVM:BaseViewModel
	{
        #region Own properties

        [Required]
		[DisplayName("Type")]
        public string? DeviceType { get; set; }

        [Required]
        [DisplayName("Number")]
        public string? FactoryNumber { get; set; }

        [DisplayName("Description")]
		public string? Description { get; set; }

        [DisplayName("Included")]
		public string? Included { get; set; }

		[DisplayName("Addition")]
		public string? Addition { get; set; }

        [Required]
        [DisplayName("Previous")]
		public DateTime PreviousDate { get; set; }

        [Required]
        [DisplayName("Next")]
		public DateTime NextDate { get; set; }

        [Required]
		[DisplayName("Created")]
		public DateTime Created { get; set; }

        [Required]
        [DisplayName("Status")]
		public StatusEnum Status { get; set; }

        [DisplayName("Picture")]
        public string? Picture { get; set; }

        #endregion

        #region IncludedPropertiesId

        [Required]
        [DisplayName("Measuring")]
        public Guid? MeasuringId { get; set; }

        [Required]
        [DisplayName("Nomination")]
        public Guid? NominationId { get; set; }

        [Required]
        [DisplayName("Operation")]
        public Guid? OperationId { get; set; }

        [Required]
        [DisplayName("Owner")]
        public Guid? OwnerId { get; set; }

        [Required]
        [DisplayName("Period")]
        public Guid? PeriodId { get; set; }

        [Required]
        [DisplayName("Category")]
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
