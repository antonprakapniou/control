namespace Control.BLL.ViewModels;

public sealed class OwnerCreatingVM
{
    #region Own properties

    public OwnerVM? OwnerVM { get; set; }
    public IEnumerable<SelectListItem>? Masters { get; set; }

    #endregion

    #region Ctor

    public OwnerCreatingVM()
    {
        OwnerVM=new();
    }

    #endregion
}
