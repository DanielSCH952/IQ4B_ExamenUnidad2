using System;
using System.Collections.Generic;

namespace ExU2.Models;

public partial class Zona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Coordenadum> Coordenada { get; set; } = new List<Coordenadum>();

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();
}
