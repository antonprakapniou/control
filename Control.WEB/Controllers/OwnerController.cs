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
        var viewModels = await _ownerService.GetAllAsync();
        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Info(Guid id)
    {
        var viewModel = await _ownerService.GetByIdAsync(id);
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var masters = await _masterService.GetSelectListAsync();

        OwnerCreatingVM ownerCreatingVM = new()
        {
            OwnerVM=new(),
            Masters=masters
        };

        return View(ownerCreatingVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OwnerCreatingVM ownerCreatingVM)
    {
        if (ModelState.IsValid)
        {
            var vm = ownerCreatingVM.OwnerVM;
            await _ownerService.CreateAsync(vm!);
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
        var viewModel = await _ownerService.GetByIdAsync(id);
        var masters = await _masterService.GetSelectListAsync();

        OwnerCreatingVM ownerCreatingVM = new()
        {
            OwnerVM=viewModel,
            Masters=masters
        };

        return View(ownerCreatingVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(OwnerCreatingVM ownerCreatingVM)
    {
        if (ModelState.IsValid)
        {
            var viewModel = ownerCreatingVM.OwnerVM;
            await _ownerService.UpdateAsync(viewModel!);
            TempData[ToastrConst.Success]=ToastrConst.UpdateSuccess;
        }

        else TempData[ToastrConst.Error]=ToastrConst.OperationError;

        return RedirectToAction(nameof(Update), new { ownerCreatingVM.OwnerVM!.Id });

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _ownerService.DeleteAsync(id);
        TempData[ToastrConst.Success]=ToastrConst.DeleteSuccess;
        return RedirectToAction(nameof(Index));
    }

    #endregion
}
