using System;
using System.Collections.Generic;

namespace ExU2.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string? NombreRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
