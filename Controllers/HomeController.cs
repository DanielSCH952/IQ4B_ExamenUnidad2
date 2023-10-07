using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExU2.Models;

namespace ExU2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.status = (User.Identity.IsAuthenticated) ? 0 : 401;
        return (User.Identity.IsAuthenticated) ? View() : RedirectToAction("NoPermitido", "Usuarios");

    }

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
