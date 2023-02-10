using Microsoft.IdentityModel.Tokens;

namespace Control.WEB.Controllers;

[Authorize]
public class HomeController : Controller
{
    #region Own fields

    private readonly IPositionService _positionService;

    #endregion

    #region Ctor

    public HomeController(IPositionService positionService)
    { 
        _positionService = positionService;
    }

    #endregion

    #region Action methods

    public async Task<IActionResult> Index()
    {
        var viewModels=await _positionService.GetAllAsync();
        var allCount=viewModels.Count();
        var currentMonthCount = viewModels.Count(_=>_.NextDateStatus.Equals(NextDateStatusEnum.CurrentMonthControl));
        var noControlCount = viewModels.Count(_ => _.NextDateStatus.Equals(NextDateStatusEnum.NeedControl));
        var invalidCount = viewModels.Count(_ => _.ValidStatus.Equals(ValidStatusEnum.Invalid));

        if (viewModels.IsNullOrEmpty()) TempData[ToastrConst.Info]="No positions";
        if (currentMonthCount>0) TempData[ToastrConst.Info]=$"{currentMonthCount} position(s) with current month control";
        if (noControlCount>0) TempData[ToastrConst.Warning]=$"For {noControlCount} positions the schedule was not completed";
        if (invalidCount>0) TempData[ToastrConst.Warning]=$"{invalidCount} position(s) need in control";

        return View();
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()=>View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
    #endregion
}