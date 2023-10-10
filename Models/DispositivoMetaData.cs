using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExU2.Models
{
    public class DispositivoMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Dirección MAC")]
        [Required(ErrorMessage = "Complete la dirección MAC")]
        public string DireccionMac { get; set; } = null!;
        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Agregue una descripción!")]
        public string Descripcion { get; set; } = null!;
        [Required(ErrorMessage = "Asigne una prioridad")]
        public int Prioridad { get; set; }
        [Required(ErrorMessage = "Establesca un estado")]
        public bool Estatus { get; set; }
        [Display(Name = "Encargado")]
        [Required(ErrorMessage = "Asigne un usuario")]
        public int? IdUsuario { get; set; }
        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Asigne una zona")]
        public int? IdZona { get; set; }

        public virtual ICollection<HistorialDispositivo> HistorialDispositivos { get; set; } = new List<HistorialDispositivo>();

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
    [ModelMetadataType(typeof(DispositivoMetaData))]
    public partial class Dispositivo { }
}