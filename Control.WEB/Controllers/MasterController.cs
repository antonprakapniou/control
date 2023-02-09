namespace Control.WEB.Controllers;

[Authorize]
public class MasterController : Controller
{
    #region Own fields

    private readonly IMasterService _service;

    #endregion

    #region Ctor

    public MasterController(IMasterService service)
    {
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
