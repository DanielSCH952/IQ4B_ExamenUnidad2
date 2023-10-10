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
        //Si necesitan la variable de hubContext deberian ingresarla por aqui:
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
            //Aqui User.IsInRole("administrador") nos ayuda a comprobar si es un administrador el que intenta ingresar o no, de no ser asi se redirecciona a NoPermitido
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
            //Esta es otra forma de validar que sea admin con un if
            if (User.IsInRole("administrador"))
            {
                return View(context.Usuarios.Include(x => x.IdRolNavigation).ToList());
            }
            return RedirectToAction("NoPermitido");
        }
        [HttpGet]
        public IActionResult EliminarUsuario(int id)
        {
            //Recibimos el parametro id y atraves de un metodo get , lo cual nos ofrece una eliminacion muy limpia desde la misma tabla
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
            //Recibe el id por metodo get desde la misma tabla para acceder a la funcion de actualizar desde un boton generan un usuario y mostrando sus parametros en el formulario
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