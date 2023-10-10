using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExU2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExU2.Controllers
{
    public class DispositivosController : Controller
    {

        private readonly SanjuanProjectDbContext _db;
        public DispositivosController(SanjuanProjectDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("administrador"))
            {
                @ViewBag.contador = 0;
                return View(_db.Dispositivos.Include(x => x.IdUsuarioNavigation).Include(x => x.IdZonaNavigation).ToList());
            }
            ViewBag.Estatus = 403;
            return RedirectToAction("NoPermitido", "Usuarios");
        }
        [HttpGet]
        public IActionResult Registrar(decimal lat, decimal lng)
        {
            if (User.IsInRole("administrador"))
            {
                ViewBag.lat = lat;
                ViewBag.lng = lng;
                ViewBag.Usuarios = new SelectList(_db.Usuarios.ToList(), "Id", "Nombre");
                ViewBag.Zonas = new SelectList(_db.Zonas.ToList(), "Id", "Nombre");
                return View();
            }
            ViewBag.Estatus = 403;
            return RedirectToAction("NoPermitido", "Usuarios");
        }
        [HttpPost]
        public IActionResult Registrar(Dispositivo dispositivo)
        {
            ViewBag.Estatus = 403;
            if (User.IsInRole("administrador") && User.Identity.IsAuthenticated)
            {
                ViewBag.Error = "No esta pasando la validacion";
                if (ModelState.IsValid)
                {
                    _db.Dispositivos.Add(dispositivo);
                    _db.SaveChanges();
                    ViewBag.Estatus = 200;
                    return RedirectToAction("Index");
                }
                // Bad Request (Faltan datos, Modelo no valido)
                ViewBag.Estatus = 400;
            }
            return View(dispositivo);
        }
        [HttpGet]
        public async Task<IActionResult> EliminarDispositivo(int id)
        {
            if (User.IsInRole("administrador"))
            {
                Dispositivo dtemp = await _db.Dispositivos.FirstOrDefaultAsync(x => x.Id == id);
                if (dtemp != null)
                {
                    _db.Dispositivos.Remove(dtemp);
                    await _db.SaveChangesAsync();
                    ViewBag.status = 200;
                    return RedirectToAction("Index");
                }
                ViewBag.Error = 404;
                return RedirectToAction("Index");
            }
            ViewBag.Estatus = 403;
            return RedirectToAction("NoPermitido", "Usuarios");
        }

        public async Task<IActionResult> ObtenerDispositivos()
        {
            return Json(await _db.Dispositivos.ToListAsync());
        }

        [HttpGet]
        public IActionResult Actualizar(int id)
        {
            if (User.IsInRole("administrador"))
            {
                Dispositivo d = _db.Dispositivos.FirstOrDefault(x => x.Id == id);
                ViewBag.Zonas = new SelectList(_db.Zonas.ToList(), "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(_db.Usuarios.ToList(), "Id", "Nombre");
                return View(d);
            }
            return RedirectToAction("NoPermitido", "Usuarios");
        }
        [HttpPost]
        public IActionResult Actualizar(int id, Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                Dispositivo dtemp = _db.Dispositivos.Include(x => x.IdUsuarioNavigation).FirstOrDefault(x => x.Id == id);
                if (dtemp != null)
                {
                    dtemp.DireccionMac = dispositivo.DireccionMac;
                    dtemp.Latitud = dispositivo.Latitud;
                    dtemp.Longitud = dispositivo.Longitud;
                    dtemp.Descripcion = dispositivo.Descripcion;
                    dtemp.Prioridad = dispositivo.Prioridad;
                    dtemp.Estatus = dispositivo.Estatus;
                    dtemp.IdUsuario = dispositivo.IdUsuario;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(dispositivo);
        }
    }
}