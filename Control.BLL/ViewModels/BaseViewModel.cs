namespace Control.BLL.ViewModels;

public abstract class BaseViewModel
{
    #region Own properties

    [Required]
    public Guid Id { get; set; }

    #endregion
}