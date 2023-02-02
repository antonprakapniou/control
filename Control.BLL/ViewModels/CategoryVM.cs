namespace Control.BLL.ViewModels;

public sealed class CategoryVM : BaseViewModel
{
    #region Own properties

    [Required]
    [DisplayName("Category")]
    public string? Name { get; set; }

    #endregion
}