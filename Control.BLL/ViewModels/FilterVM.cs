namespace Control.BLL.ViewModels;

public sealed class FilterVM
{
    #region Own properties
    
    [DisplayName("Category")] public Guid? CategoryId { get; set; } = default;
    [DisplayName("Measuring")] public Guid? MeasuringId { get; set; } = default;
    [DisplayName("Nomination")] public Guid? NominationId { get; set; } = default;
    [DisplayName("Operation")] public Guid? OperationId { get; set; } = default;
    [DisplayName("Owner")] public Guid? OwnerId { get; set; } = default;
    [DisplayName("Period")] public Guid? PeriodId { get; set; } = default;
    [DisplayName("Number")] public string? Number { get; set; } = default;
    [DisplayName("Month")] public int? Month { get; set; } = default; 

    #endregion
}
