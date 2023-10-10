using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExU2.Models;

namespace ExU2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            ViewBag.Estatus = 200;
            return View();
        }
        ViewBag.Estatus = 401;
        return RedirectToAction("NoPermitido", "Usuarios");

    }

    public IActionResult Privacy()
    {
        return View();
    }
}
