using Microsoft.AspNetCore.Mvc.Rendering;

namespace Control.BLL.ViewModels
{
    public sealed class PositionCreatingVM
    {
        public PositionVM? PositionVM { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IEnumerable<SelectListItem>? Measurings { get; set; }
        public IEnumerable<SelectListItem>? Nominations { get; set; }
        public IEnumerable<SelectListItem>? Operations { get; set; }
        public IEnumerable<SelectListItem>? Owners { get; set; }
        public IEnumerable<SelectListItem>? Periods { get; set; }
    }
}
