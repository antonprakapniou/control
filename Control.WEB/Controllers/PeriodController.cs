﻿namespace Control.WEB.Controllers;

public sealed class PeriodController : Controller
{
    #region Own fields

    private readonly ILogger<PeriodController> _logger;
    private readonly IPeriodService _service;

    #endregion

    #region Ctor

    public PeriodController(
        ILogger<PeriodController> logger,
        IPeriodService service)
    {
        _logger=logger;
        _service=service;
    }

    #endregion

    #region Action methods

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModels = await _service.GetAllAsync();
        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PeriodVM viewModel)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(viewModel);
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
        var viewModel = await _service.GetByIdAsync(id);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PeriodVM viewModel)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(viewModel);
            TempData[ToastrConst.Success]=ToastrConst.UpdateSuccess;
        }
        else TempData[ToastrConst.Error]=ToastrConst.OperationError;
        return RedirectToAction(nameof(Update), new { viewModel.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        TempData[ToastrConst.Success]=ToastrConst.DeleteSuccess;
        return RedirectToAction(nameof(Index));
    }

    #endregion
}
