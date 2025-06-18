using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace obligatorioProg3.ViewModels
{
    public class RegistroUsuarioViewModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required]
        public int Cedula { get; set; }

        [Required]
        public string NombreReal { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
    }
}