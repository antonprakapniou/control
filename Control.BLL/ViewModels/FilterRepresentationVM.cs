using Microsoft.AspNetCore.Mvc;

namespace Control.BLL.ViewModels;

public sealed class FilterRepresentationVM
{
    #region Own properties

    [BindProperty]
    public FilterVM? Filter { get; set; }

    [BindProperty]
    public IEnumerable<PositionVM>? Positions { get; set; }
    public IEnumerable<SelectListItem>? Categories { get; set; }
    public IEnumerable<SelectListItem>? Measurings { get; set; }
    public IEnumerable<SelectListItem>? Nominations { get; set; }
    public IEnumerable<SelectListItem>? Operations { get; set; }
    public IEnumerable<SelectListItem>? Owners { get; set; }
    public IEnumerable<SelectListItem>? Periods { get; set; }

    #endregion
}
