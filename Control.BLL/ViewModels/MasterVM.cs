namespace Control.BLL.ViewModels;

public sealed class MasterVM
{
    #region Own properties
    public string? Id { get; set; }

    [Required]
    [DisplayName("Master")]
    public string? Name { get; set; }

    [DisplayName("Master email")]
    public string? Email { get; set; }

    [DisplayName("Master phone")]
    public string? PhoneNumber { get; set; }

    #endregion
}
