using System;
using System.Collections.Generic;

namespace ExU2.Models;

public partial class Coordenada
{
    public int Id { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public int? IdZona { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
