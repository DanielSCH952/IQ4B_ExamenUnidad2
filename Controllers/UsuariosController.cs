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
        public UsuariosController(SanjuanProjectDbContext dbContext)
        {
            context = dbContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario u)
        {

            Usuario utemp = context.Usuarios.Include(x => x.IdRolNavigation)
                            .FirstOrDefault(x => x.Username == u.Username
                            && x.Password == u.Password)!;

            if (utemp != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("username", utemp.Username!));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, utemp.Username!));
                claims.Add(new Claim(ClaimTypes.Role, utemp.IdRolNavigation.NombreRol!));
                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");


            }
            ViewBag.Error = "Usuario y/o password incorrectos";
            return View(u);
        }
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return (User.IsInRole("administrador")) ? View() : RedirectToAction("NoPermitido");
        }
        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario u)
        {

            if (ModelState.IsValid)
            {
                context.Usuarios.Add(u);
                context.SaveChanges();
                return RedirectToAction("MostrarUsuarios");
            }
            ViewBag.Error = "Modelo no valido";
            return View(u);
        }
        [HttpGet]
        public IActionResult MostrarUsuarios()
        {

            if (User.IsInRole("administrador"))
            {
                return View(context.Usuarios.Include(x => x.IdRolNavigation).ToList());
            }
            ViewBag.status = 403;
            return RedirectToAction("NoPermitido");
        }
        [HttpGet]
        public IActionResult EliminarUsuario(int id)
        {
            Usuario utemp = context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (utemp == null)
            {
                ViewBag.Error = "No se pudo eliminar";
            }
            else
            {
                context.Remove(utemp);
                context.SaveChanges();
            }
            return RedirectToAction("MostrarUsuarios");
        }
        [HttpGet]
        public IActionResult ActualizarUsuario(int id)
        {
            Usuario utemp = context.Usuarios.Include(x => x.IdRolNavigation).FirstOrDefault(user => user.Id == id);
            return View(utemp);
        }
        [HttpPost]
        public IActionResult ActualizarUsuario(int id, Usuario u)
        {
            ViewBag.Error = "Error del modelo";
            if (ModelState.IsValid)
            {
                Usuario utemp = context.Usuarios.FirstOrDefault(x => x.Id == id);
                if (utemp != null)
                {
                    utemp.Nombre = u.Nombre;
                    utemp.ApellidoMaterno = u.ApellidoMaterno;
                    utemp.ApellidoPaterno = u.ApellidoPaterno;
                    utemp.Edad = u.Edad;
                    utemp.Sexo = u.Sexo;
                    utemp.Username = u.Username;
                    utemp.Password = u.Password;
                    context.SaveChanges();
                    return RedirectToAction("MostrarUsuarios");
                }
                ViewBag.Error = "Usuario no encontrado error";
            }

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