namespace Control.BLL.ViewModels;

public abstract class BaseVM
{
    #region Own properties

    [Required]
    public Guid Id { get; set; }

    #endregion
}