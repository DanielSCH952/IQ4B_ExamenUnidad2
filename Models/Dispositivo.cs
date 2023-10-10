using System;
using System.Collections.Generic;

namespace ExU2.Models;

public partial class Dispositivo
{
    public int Id { get; set; }

    public string DireccionMac { get; set; } = null!;

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Prioridad { get; set; }

    public bool Estatus { get; set; }

    public int? IdZona { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<HistorialDispositivo> HistorialDispositivos { get; set; } = new List<HistorialDispositivo>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
