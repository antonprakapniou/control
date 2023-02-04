namespace Control.WEB.Controllers;

public sealed class PositionController : Controller
{
    private const string _partialPath = AppConstants.PositionPartialPath;

    #region Own fields

    private readonly ILogger<PositionController> _logger;
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
        ILogger<PositionController> logger,
        IPositionService positionService,
        ICategoryService categoryService,
        IMeasuringService measuringService,
        INominationService nominationService,
        IOperationService operationService,
        IOwnerService ownerService,
        IPeriodService periodService,
        IFileManager fileManager)
    {
        _logger=logger;
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
        try
        {
            var vms = await _positionService.GetAllAsync();
            return View(vms);
        }

        catch (ObjectNotFoundException ex)
        {
            string message = ex.Message;
            _logger.LogError(message);
            return NotFound(message);
        }

        catch (Exception ex)
        {
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Info(Guid id)
    {
        var vm = await _positionService.GetByIdAsync(id);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            var measurings = await _measuringService.GetAllAsync();
            var nominations = await _nominationService.GetAllAsync();
            var operations = await _operationService.GetAllAsync();
            var owners = await _ownerService.GetAllAsync();
            var periods = await _periodService.GetAllAsync();

            PositionCreatingVM positionCreatingVM = new()
            {
                PositionVM=new(),

                Categories=categories.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Measurings=measurings.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Code
                }),

                Nominations=nominations.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Operations=operations.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Owners=owners.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.ShortName
                }),

                Periods=periods.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                })
            };

            return View(positionCreatingVM);
        }

        catch (Exception ex)
        {
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PositionCreatingVM positionCreatingVM)
    {
        try
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
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrCreateSuccess;
                return RedirectToAction(nameof(Index));
            }

            else return RedirectToAction();
        }

        catch (Exception ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrCreateError;
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            var vm = await _positionService.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            var measurings = await _measuringService.GetAllAsync();
            var nominations = await _nominationService.GetAllAsync();
            var operations = await _operationService.GetAllAsync();
            var owners = await _ownerService.GetAllAsync();
            var periods = await _periodService.GetAllAsync();

            PositionCreatingVM positionCreatingVM = new()
            {
                PositionVM=vm,

                Categories=categories.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Measurings=measurings.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Code
                }),

                Nominations=nominations.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Operations=operations.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                }),

                Owners=owners.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.ShortName,
                }),

                Periods=periods.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                })
            };

            return View(positionCreatingVM);
        }

        catch (ObjectNotFoundException ex)
        {
            string message = ex.Message;
            _logger.LogError(message);
            return NotFound(message);
        }

        catch (Exception ex)
        {
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PositionCreatingVM positionCreatingVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vm = positionCreatingVM.PositionVM;
                var vmFromDb = await _positionService.GetByIdAsync(vm!.Id);
                var oldPicture = vmFromDb.Picture;
                var files = HttpContext.Request.Form.Files;

                if (files.Count!=0)
                {
                    if (oldPicture is not null) _fileManager.Delete(oldPicture!, _partialPath);
                    _fileManager.Load(files, _partialPath);
                    vm!.Picture=_fileManager.FileName;
                }

                else vm.Picture=oldPicture;

                await _positionService.UpdateAsync(vm!);
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrUpdateSuccess;
                return RedirectToAction(nameof(Update), new { id = vm.Id });
            }

            return RedirectToAction();
        }

        catch (Exception ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrUpdateError;
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePicture(PositionCreatingVM positionCreatingVM)
    {
        try
        {
            var vm = await _positionService.GetByIdAsync(positionCreatingVM.PositionVM!.Id);

            if (vm.Picture is not null)
            {
                _fileManager.Delete(vm.Picture, _partialPath);
                vm.Picture = null;
                await _positionService.UpdateAsync(vm);
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrPictureDeleteSuccess;
            }

            else TempData[AppConstants.ToastrError]=AppConstants.ToastrPictureNotFound;
            return RedirectToAction(nameof(Update), new { id = vm.Id });
        }

        catch (ObjectNotFoundException ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrPictureDeleteError;
            string message = ex.Message;
            _logger.LogError(message);
            return NotFound(message);
        }

        catch (Exception ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrPictureDeleteError;
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var model = await _positionService.GetByIdAsync(id);
            if (model.Picture is not null) _fileManager.Delete(model.Picture, _partialPath);
            await _positionService.DeleteAsync(id);
            TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrDeleteSuccess;
            return RedirectToAction(nameof(Index));
        }

        catch (ObjectNotFoundException ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrDeleteError;
            string message = ex.Message;
            _logger.LogError(message);
            return NotFound(message);
        }

        catch (Exception ex)
        {
            TempData[AppConstants.ToastrError]=AppConstants.ToastrDeleteError;
            string message = ex.Message;
            _logger.LogError(message);
            return BadRequest(message);
        }
    }

    #endregion
}