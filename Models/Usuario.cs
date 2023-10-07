using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExU2.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre no debe ir vacio")]
    public string Nombre { get; set; } = null!;
    [Display(Name = "Apellido Paterno")]
    [Required(ErrorMessage = "El apellido paterno no debe ir vacio")]
    public string ApellidoPaterno { get; set; } = null!;
    [Display(Name = "Apellido Materno")]
    [Required(ErrorMessage = "El apellido materno no debe ir vacio")]
    public string ApellidoMaterno { get; set; } = null!;
    [Required(ErrorMessage = "La edad no debe ir vacio")]
    public int Edad { get; set; }
    [Required(ErrorMessage = "El sexo no debe ir vacio")]
    public string Sexo { get; set; } = null!;
    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "El usuario debe ser llenado")]
    public string Username { get; set; } = null!;
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "Proveea la contraseña")]
    public string Password { get; set; } = null!;

    public int? IdRol { get; set; }

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();

    public virtual Rol? IdRolNavigation { get; set; }
}
