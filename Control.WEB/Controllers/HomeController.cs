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

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()=>View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
    #endregion
}