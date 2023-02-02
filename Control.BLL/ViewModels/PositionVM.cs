namespace Control.BLL.ViewModels;

public sealed class PositionVM : BaseViewModel
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
    [DisplayName("Advice")]
    public DateTime AdviceDate { get; set; }

    [Required]
    [DisplayName("Created")]
    public DateTime Created { get; set; }

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

    public MeasuringVM? Measuring { get; set; }
    public NominationVM? Nomination { get; set; }
    public OperationVM? Operation { get; set; }
    public OwnerVM? Owner { get; set; }
    public PeriodVM? Period { get; set; }
    public CategoryVM? Category { get; set; }

    #endregion

    #region NotSet

    [Required]
    [DisplayName("Status")]
    public StatusEnum Status { get => SetStatus(NextDate); }

    [Required]
    [DisplayName("Need")]
    public DateTime NeedDate { get => (Period is not null) ? PreviousDate.AddMonths(Period.Month) : PreviousDate; }

    #endregion

    #region Methods

    private static StatusEnum SetStatus(DateTime nextDate)
    {
        DateTime currentDate = DateTime.Now;

        if (currentDate>nextDate) return StatusEnum.Invalid;
        else if (currentDate.AddMonths(2)>nextDate&&currentDate.Month.Equals(nextDate.Month)) return StatusEnum.CurrentMonthControl;
        else if (currentDate.AddMonths(2)>nextDate&&currentDate.AddMonths(1).Month.Equals(nextDate.Month)) return StatusEnum.NextMonthControl;
        else if (currentDate<=nextDate) return StatusEnum.Valid;
        else return StatusEnum.Indefined;
    }

    #endregion
}
