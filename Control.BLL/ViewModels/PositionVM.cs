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
    [DisplayName("Need")]
    public DateTime NeedDate { get; set; }

    [Required]
    [DisplayName("Next")]
    public DateTime NextDate { get; set; }

    [Required]
    [DisplayName("Advice")]
    public DateTime AdviceDate { get; set; }

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

    [DisplayName("Status")]
    public ValidStatusEnum ValidStatus { get => SetValidStatus(NeedDate); }

    #endregion

    #region Methods

    private static ValidStatusEnum SetValidStatus( DateTime needDate)
    {
        DateTime currentDate = DateTime.Now;

        if (currentDate>needDate) return ValidStatusEnum.Invalid;
        else if (currentDate.AddMonths(2)>needDate&&currentDate.Month.Equals(needDate.Month)) return ValidStatusEnum.CurrentMonthControl;
        else if (currentDate.AddMonths(2)>needDate&&currentDate.AddMonths(1).Month.Equals(needDate.Month)) return ValidStatusEnum.NextMonthControl;
        else if (currentDate<=needDate) return ValidStatusEnum.Valid;
        else return ValidStatusEnum.Indefined;
    }

    #endregion
}
