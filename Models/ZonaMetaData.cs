using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExU2.Models
{
    public class ZonaMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Zona")]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Coordenada> Coordenada { get; set; } = new List<Coordenada>();

        public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();
    }
    [ModelMetadataType(typeof(ZonaMetaData))]
    partial class Zona { }
}