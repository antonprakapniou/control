namespace Control.BLL.ViewModels;

public sealed class IdentityMasterVM:BaseViewModel
{
    #region Own properties

    [Required]
    [DisplayName("Master")]
    public string? UserName { get; set; }

    [DisplayName("Master email")]
    public string? Email { get; set; }

    [DisplayName("Master phone")]
    public string? PhoneNumber { get; set; }

    #endregion
}
