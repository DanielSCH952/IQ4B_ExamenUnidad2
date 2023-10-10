using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ExU2.Models
{
    public class UsuarioMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; } = null!;
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; } = null!;

        public int Edad { get; set; }

        public string Sexo { get; set; } = null!;
        [Display(Name = "Usuario")]
        public string Username { get; set; } = null!;
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;
        [Display(Name = "Rol")]
        public int? IdRol { get; set; }

        public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();

        public virtual Rol? IdRolNavigation { get; set; }
    }
    [ModelMetadataType(typeof(UsuarioMetaData))]
    public partial class Usuario { }
}