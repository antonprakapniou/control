using Newtonsoft.Json;

namespace Control.WEB.Controllers;

public sealed class PositionController : Controller
{
    private const string _partialPath = GeneralConst.PositionPartialPath;

    #region Own fields

    private readonly IPositionService _positionService;
    private readonly ICategoryService _categoryService;
    private readonly IMeasuringService _measuringService;
    private readonly INominationService _nominationService;
    private readonly IOperationService _operationService;
    private readonly IOwnerService _ownerService;
    private readonly IPeriodService _periodService;
    private readonly IFileManager _fileManager;

    #endregion

    #region Ctor

    public PositionController(
        IPositionService positionService,
        ICategoryService categoryService,
        IMeasuringService measuringService,
        INominationService nominationService,
        IOperationService operationService,
        IOwnerService ownerService,
        IPeriodService periodService,
        IFileManager fileManager)
    {
        _positionService=positionService;
        _categoryService=categoryService;
        _measuringService=measuringService;
        _nominationService=nominationService;
        _operationService=operationService;
        _ownerService=ownerService;
        _periodService=periodService;
        _fileManager=fileManager;
    }

    #endregion

    #region Action methods

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModels = await _positionService.GetAllAsync();
        return View(viewModels);
    }
    
    [HttpGet]
    public async Task<IActionResult> Filter(string response)
    {
        var representation = new FilterRepresentationVM();

        if (response is not null)
        {
            var filterResponse = JsonConvert.DeserializeObject<FilterResponse>(response!);
            representation.Filter=filterResponse!.Filter;
            var positionList = new List<PositionVM>();

            foreach (var item in filterResponse.Ids!) positionList.Add(await _positionService.GetByIdAsync(item));

            representation.Positions = positionList;
        }

        else
        {
            representation.Positions = new List<PositionVM>();
        }

        representation.Categories=await _categoryService.GetSelectListAsync();
        representation.Measurings=await _measuringService.GetSelectListAsync();
        representation.Nominations=await _nominationService.GetSelectListAsync();
        representation.Operations=await _operationService.GetSelectListAsync();
        representation.Owners=await _ownerService.GetSelectListAsync();
        representation.Periods=await _periodService.GetSelectListAsync();

        return View(representation);
    }

    [HttpPost]
    public async Task<IActionResult> FilterPost(FilterRepresentationVM representation)
    {
        representation.Positions=await _positionService.GetAllByFilterAsync(representation.Filter!);
        Guid[] ids = representation.Positions.Select(x => x.Id).ToArray();
        FilterResponse filterResponce = new() { Filter=representation.Filter, Ids=ids };
        string jsonResponse = JsonConvert.SerializeObject(filterResponce);
        return RedirectToAction("Filter", new { Response=jsonResponse });
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
        PositionCreatingVM positionCreatingVM = new()
        {
            PositionVM = new(),
            Categories = await _categoryService.GetSelectListAsync(),
            Measurings= await _measuringService.GetSelectListAsync(),
            Nominations=await _nominationService.GetSelectListAsync(),
            Operations=await _operationService.GetSelectListAsync(),
            Owners=await _ownerService.GetSelectListAsync(),
            Periods=await _periodService.GetSelectListAsync(),
        };

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
        PositionCreatingVM positionCreatingVM = new()
        {
            PositionVM=await _positionService.GetByIdAsync(id),
            Categories = await _categoryService.GetSelectListAsync(),
            Measurings= await _measuringService.GetSelectListAsync(),
            Nominations=await _nominationService.GetSelectListAsync(),
            Operations=await _operationService.GetSelectListAsync(),
            Owners=await _ownerService.GetSelectListAsync(),
            Periods=await _periodService.GetSelectListAsync(),
        };

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