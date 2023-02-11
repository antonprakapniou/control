namespace Control.WEB.Controllers;

[Authorize]
public sealed class PositionController : Controller
{
    private const string _partialPath = GeneralConst.PositionPartialPath;

    #region Own fields

    private readonly IPositionService _positionService;
    private readonly IFileManager _fileManager;

    #endregion

    #region Ctor

    public PositionController(
        IPositionService positionService,
        IFileManager fileManager)
    {
        _positionService=positionService;
        _fileManager=fileManager;
    }

    #endregion

    #region Action methods

    [HttpGet]
    public async Task<IActionResult> Index(string? jsonFilter)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!.ToUpper();
        var allPositions = await _positionService.GetAllAsync();
        var positions = allPositions;        

        FilterRepresentationVM representation = new();
        representation.Filter = (jsonFilter is null) ? new() : JsonConvert.DeserializeObject<FilterVM>(jsonFilter!);
        var filterPositions = (jsonFilter is null) ? allPositions : await _positionService.GetAllByFilterAsync(representation.Filter!);
        var ownerPositions = (User.IsInRole(RoleConst.Admin)) ? filterPositions : filterPositions.Where(_=>_.Owner!.NormMasterId!.Equals(userId));
        representation.Positions=ownerPositions;
        await _positionService.SetFilterSelectList(representation);

        #region Notifications

        var allCount = representation.Positions.Count();
        var currentMonthCount = representation.Positions.Count(_ => _.NextDateStatus.Equals(NextDateStatusEnum.CurrentMonthControl));
        var noControlCount = representation.Positions.Count(_ => _.NextDateStatus.Equals(NextDateStatusEnum.NeedControl));
        var invalidCount = representation.Positions.Count(_ => _.ValidStatus.Equals(ValidStatusEnum.Invalid));

        if (!representation.Positions.Any()) TempData[ToastrConst.Info]="No positions";
        if (currentMonthCount>0) TempData[ToastrConst.Info]=$"{currentMonthCount} position(s) with current month control";
        if (noControlCount>0) TempData[ToastrConst.Warning]=$"For {noControlCount} positions the schedule was not completed";
        if (invalidCount>0) TempData[ToastrConst.Warning]=$"{invalidCount} position(s) need in control";

        # endregion

        return View(representation);
    }

    [HttpPost]
    public IActionResult Index(FilterRepresentationVM representation)
    {
        string jsonFilter = JsonConvert.SerializeObject(representation.Filter);
        return RedirectToAction(nameof(Index), new { JsonFilter = jsonFilter });
    }

    [HttpGet]
    public async Task<IActionResult> Info(Guid id)
    {
        var viewModel = await _positionService.GetByIdAsync(id);
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        PositionCreatingVM positionCreatingVM = new();
        await _positionService.SetPositionSelectList(positionCreatingVM);
        return View(positionCreatingVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PositionCreatingVM positionCreatingVM)
    {
        if (ModelState.IsValid)
        {
            var viewModel = positionCreatingVM.PositionVM;

            var files = HttpContext.Request.Form.Files;
            if (files.Count!=0)
            {
                _fileManager.Load(files, _partialPath);
                viewModel!.Picture=_fileManager.FileName;
            }

            await _positionService.CreateAsync(viewModel!);
            TempData[ToastrConst.Success]=ToastrConst.CreateSuccess;
            return RedirectToAction(nameof(Index));
        }

        else
        {
            TempData[ToastrConst.Error]=ToastrConst.OperationError;
            return RedirectToAction();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        PositionCreatingVM positionCreatingVM = new();
        var viewModel = await _positionService.GetByIdAsync(id);
        positionCreatingVM.PositionVM=viewModel;
        await _positionService.SetPositionSelectList(positionCreatingVM);
        return View(positionCreatingVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PositionCreatingVM positionCreatingVM)
    {
        if (ModelState.IsValid)
        {
            var viewModel = positionCreatingVM.PositionVM;
            var vmFromDb = await _positionService.GetByIdAsync(viewModel!.Id);
            var oldPicture = vmFromDb.Picture;
            var files = HttpContext.Request.Form.Files;

            if (files.Count!=0)
            {
                if (oldPicture is not null) _fileManager.Delete(oldPicture!, _partialPath);
                _fileManager.Load(files, _partialPath);
                viewModel!.Picture=_fileManager.FileName;
            }

            else viewModel.Picture=oldPicture;

            await _positionService.UpdateAsync(viewModel!);
            TempData[ToastrConst.Success]=ToastrConst.UpdateSuccess;
            return RedirectToAction(nameof(Update), new { id = viewModel.Id });
        }

        else TempData[ToastrConst.Error]=ToastrConst.InvalidModel;
        return RedirectToAction(nameof(Update), new { Id = positionCreatingVM.PositionVM!.Id }); ;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePicture(PositionCreatingVM positionCreatingVM)
    {
        var id = positionCreatingVM.PositionVM!.Id;
        var viewModel = await _positionService.GetByIdAsync(id);
        if (viewModel.Picture is not null)
        {
            viewModel.Picture = null;
            await _positionService.UpdateAsync(viewModel);
        }                
        
        TempData[ToastrConst.Success]="Picture deleted successfully";
        return RedirectToAction(nameof(Update), new { Id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var model = await _positionService.GetByIdAsync(id);
        if (model.Picture is not null) _fileManager.Delete(model.Picture, _partialPath);
        await _positionService.DeleteAsync(id);
        TempData[ToastrConst.Success]=ToastrConst.DeleteSuccess;
        return RedirectToAction(nameof(Index));
    }

    #endregion
}