namespace Control.WEB.Controllers;

public sealed class OwnerController : Controller
{
    #region Own fields

    private readonly ILogger<OwnerController> _logger;
    private readonly IOwnerService _ownerService;
    private readonly IMasterService _masterService;

    #endregion

    #region Ctor

    public OwnerController(
        ILogger<OwnerController> logger,
        IOwnerService service,
        IMasterService masterService)
    {
        _logger=logger;
        _ownerService=service;
        _masterService=masterService;
    }

    #endregion

    #region Action methods

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var vms = await _ownerService.GetAllAsync();
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
        var vm = await _ownerService.GetByIdAsync(id);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var masters = await _masterService.GetAllAsync();

            OwnerCreatingVM ownerCreatingVM = new()
            {
                OwnerVM=new(),

                Masters=masters.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                })
            };

            return View(ownerCreatingVM);
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
    public async Task<IActionResult> Create(OwnerCreatingVM ownerCreatingVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vm = ownerCreatingVM.OwnerVM;
                await _ownerService.CreateAsync(vm!);
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrCreateSuccess;
                return RedirectToAction("Index");
            }

            else return View();

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
            var vm = await _ownerService.GetByIdAsync(id);
            var masters = await _masterService.GetAllAsync();

            OwnerCreatingVM ownerCreatingVM = new()
            {
                OwnerVM=vm,

                Masters=masters.Select(_ => new SelectListItem
                {
                    Value=_.Id.ToString(),
                    Text=_.Name
                })
            };

            return View(ownerCreatingVM);
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
    public async Task<IActionResult> Update(OwnerCreatingVM ownerCreatingVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vm = ownerCreatingVM.OwnerVM;
                await _ownerService.UpdateAsync(vm!);
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrUpdateSuccess;
                return RedirectToAction(nameof(Update), new { id = vm!.Id });
            }

            else return View();
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
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _ownerService.DeleteAsync(id);
            TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrDeleteSuccess;
            return RedirectToAction("Index");
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
