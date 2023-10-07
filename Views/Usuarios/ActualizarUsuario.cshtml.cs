using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExU2.Views.Usuarios
{
    public class ActualizarUsuario : PageModel
    {
        private readonly ILogger<ActualizarUsuario> _logger;

        public ActualizarUsuario(ILogger<ActualizarUsuario> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}