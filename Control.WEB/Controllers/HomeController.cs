﻿namespace Control.WEB.Controllers;

[Authorize]
public class HomeController : Controller
{
    public HomeController() { }

    public IActionResult Index() => View();

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}