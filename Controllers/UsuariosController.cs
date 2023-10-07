using ExU2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ExU2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SanjuanProjectDbContext context;
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario u)
        {

            if (ModelState.IsValid)
            {
                Usuario utemp = context.Usuarios.Include(x => x.IdRolNavigation)
                                .FirstOrDefault(x => x.Username == u.Username
                                && x.Password == u.Password)!;
                if (utemp != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("username", utemp.Username!));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, utemp.Username!));
                    claims.Add(new Claim(ClaimTypes.Role, utemp.IdRolNavigation.NombreRol));
                    ClaimsIdentity identity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }

            }
            ViewBag.Error = "Usuario y/o password incorrectos";
            return View(u);
        }

        public IActionResult NoPermitido()
        {
            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}