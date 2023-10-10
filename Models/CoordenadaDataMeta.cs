using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExU2.Models
{
    public class CoordenadaDataMeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }
        [Display(Name = "Zona")]
        public int? IdZona { get; set; }

        public virtual Zona? IdZonaNavigation { get; set; }
    }
    [ModelMetadataType(typeof(CoordenadaDataMeta))]
    partial class Coordenada { }
}