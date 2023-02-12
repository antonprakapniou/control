namespace Control.BLL.ViewModels;

public sealed class OperationVM : BaseVM
{
    #region Own properties

    [Required]
    [DisplayName("Operation")]
    public string? Name { get; set; }

    #endregion
}