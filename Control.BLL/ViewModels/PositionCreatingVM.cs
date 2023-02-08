namespace Control.BLL.ViewModels
{
    public sealed class PositionCreatingVM
    {
        #region Own properties

        public PositionVM? PositionVM { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IEnumerable<SelectListItem>? Measurings { get; set; }
        public IEnumerable<SelectListItem>? Nominations { get; set; }
        public IEnumerable<SelectListItem>? Operations { get; set; }
        public IEnumerable<SelectListItem>? Owners { get; set; }
        public IEnumerable<SelectListItem>? Periods { get; set; }

        #endregion

        #region Ctor

        public PositionCreatingVM()
        {
            PositionVM=new();
        }

        #endregion
    }
}
