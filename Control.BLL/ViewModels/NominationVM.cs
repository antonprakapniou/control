namespace Control.BLL.ViewModels;

public sealed class NominationVM : BaseVM
{
    #region Own properties

    [Required]
    [DisplayName("Nomination")]
    public string? Name { get; set; }

    #endregion
}
