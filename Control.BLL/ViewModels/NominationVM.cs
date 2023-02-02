namespace Control.BLL.ViewModels;

public sealed class NominationVM : BaseViewModel
{
    #region Own properties

    [Required]
    [DisplayName("Nomination")]
    public string? Name { get; set; }

    #endregion
}
