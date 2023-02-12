namespace Control.BLL.ViewModels;

public sealed class MeasuringVM : BaseVM
{
    #region Own properties

    [Required]
    [DisplayName("Name")]
    public string? Name { get; set; }

    [Required]
    [DisplayName("Code")]
    public string? Code { get; set; }

    [DisplayName("Measuring")]
    public string? FullName { get => $"{Code} {Name}".TrimEnd(); }

    #endregion
}