namespace Control.BLL.ViewModels;

public sealed class PeriodVM : BaseViewModel
{
    private const int _defaultMonth = 12;

    #region Own properties

    [Required]
    [DisplayName("Name")]
    public string? Name { get; set; }

    public int Month { get => (int.TryParse(Name, out int month)||Name is null) ? month : _defaultMonth; }

    #endregion
}
