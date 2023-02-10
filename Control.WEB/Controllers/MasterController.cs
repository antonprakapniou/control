namespace Control.WEB.Controllers;

[Authorize(Roles =RoleConst.Admin)]
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
    public async Task<IActionResult> Delete(string id)
    {
        var viewModel=await _service.GetByIdAsync(id);

        if (viewModel.Name!.Equals(RoleConst.AdminName))
        {
            TempData[ToastrConst.Error]="It cannot be removed";
        }

        else
        {
            await _service.DeleteAsync(id);
            TempData[ToastrConst.Success]=ToastrConst.DeleteSuccess;
        }

        return RedirectToAction(nameof(Index));
    }

    #endregion
}
