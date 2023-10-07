using System;
using System.Collections.Generic;

namespace ExU2.Models;

public partial class HistorialDispositivo
{
    public int Id { get; set; }

    public decimal Cantidad { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime? Fin { get; set; }

    public int? IdDispositivo { get; set; }

    public virtual Dispositivo? IdDispositivoNavigation { get; set; }
}
