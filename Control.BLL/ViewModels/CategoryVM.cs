namespace Control.BLL.ViewModels;

public sealed class CategoryVM : BaseVM
{
    #region Own properties

    [Required]
    [DisplayName("Category")]
    public string? Name { get; set; }

    #endregion
}