namespace Control.BLL.ViewModels;

public sealed class MasterVM:BaseViewModel
{
    #region Own properties

    [Required]
    [DisplayName("Name")]
    public string? Name { get; set; }

    [DisplayName("Master phone")]
    public string? Phone { get; set; }

    [DisplayName("Email")]
    public string? Email { get; set; }

    #endregion

}
