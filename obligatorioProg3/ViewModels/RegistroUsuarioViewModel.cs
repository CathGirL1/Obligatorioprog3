using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace obligatorioProg3.ViewModels
{
    public class RegistroUsuarioViewModel
    {
        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [Display(Name = "Nombre de usuario")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [EmailAddress(ErrorMessage = "El formato del email es inválido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres", MinimumLength = 8)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [Display(Name = "Cédula")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [Display(Name = "Nombre real")]
        public string NombreReal { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Se debe rellenar este campo")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        
    }
}