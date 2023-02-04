namespace Control.BLL.ViewModels;

public sealed class MasterVM:BaseViewModel
{
    #region Own properties

    [Required]
    [DisplayName("Master")]
    public string? Name { get; set; }

    [DisplayName("Master phone")]
    public string? Phone { get; set; }

    [DisplayName("Master email")]
    public string? Email { get; set; }

    #endregion

}
