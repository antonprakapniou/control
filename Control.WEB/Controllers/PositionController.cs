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
        var representation = new FilterRepresentationVM();

        if (jsonFilter is not null)
        {
            var filter = JsonConvert.DeserializeObject<FilterVM>(jsonFilter!);
            representation.Filter=filter;
            representation.Positions=await _positionService.GetAllByFilterAsync(representation.Filter!);
        }

        else
        {
            representation.Positions = await _positionService.GetAllAsync();
        }

        await _positionService.SetFilterSelectList(representation);
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
        return RedirectToAction(nameof(Update), new { id = positionCreatingVM.PositionVM!.Id }); ;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePicture(PositionCreatingVM positionCreatingVM)
    {
        var viewModel = await _positionService.GetByIdAsync(positionCreatingVM.PositionVM!.Id);

        if (viewModel.Picture is not null)
        {
            _fileManager.Delete(viewModel.Picture, _partialPath);
            viewModel.Picture = null;
            await _positionService.UpdateAsync(viewModel);
            TempData[ToastrConst.Success]=ToastrConst.DeleteSuccess;
        }

        else TempData[ToastrConst.Error]=ToastrConst.OperationError;
        return RedirectToAction(nameof(Update), new { id = viewModel.Id });
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