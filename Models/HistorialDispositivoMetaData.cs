using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExU2.Models
{
    public class HistorialDispositivoMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "¡Indique una cantidad!")]
        [Display(Name = "Cantidad Inicial")]
        public decimal CantidadInicial { get; set; }
        [Display(Name = "Hora de Inicio")]
        [Required(ErrorMessage = "Asigne una hora de inicio")]
        public DateTime Inicio { get; set; }
        [Display(Name = "Hora de fin")]
        public DateTime? Fin { get; set; }
        [Display(Name = "Dispositivo")]
        [Required(ErrorMessage = "Se debe asignar un dispositivo")]
        public int? IdDispositivo { get; set; }
        [Display(Name = "Cantidad Final")]
        public decimal? CantidadFinal { get; set; }
        public virtual Dispositivo? IdDispositivoNavigation { get; set; }
    }
    [ModelMetadataType(typeof(HistorialDispositivoMetaData))]
    partial class HistorialDispositivo { }
}