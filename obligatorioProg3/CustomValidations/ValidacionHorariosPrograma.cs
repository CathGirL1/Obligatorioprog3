using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using obligatorioProg3.Models;
using System.ComponentModel.DataAnnotations;

namespace obligatorioProg3.CustomValidations
{
    public class ValidacionHorariosPrograma : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var horarioPrograma = (horario_programa)validationContext.ObjectInstance;
            string diaElegido = horarioPrograma.dia;

            if (value == null || horarioPrograma == null || string.IsNullOrEmpty(diaElegido))
            {
                return new ValidationResult("El valor proporcionado no es válido.");
            }

            TimeSpan horaElegida = (TimeSpan)value;

            using (var db = new vozDelEsteBsdEntities())
            {
                List<horario_programa> horariosExistentesDiaElegido = db.horario_programa
                    .Where(h => h.dia == diaElegido).ToList();

                foreach (var horarios in horariosExistentesDiaElegido)
                {
                    if (horaElegida > horarios.horaInicio && horaElegida < horarios.horaFinal)
                    {
                        return new ValidationResult("La hora elegida está dentro de un rango existente.");
                    }
                }
            }

            return ValidationResult.Success; // La hora elegida no está en conflicto con los horarios existentes
        }

        public override string FormatErrorMessage(string name)
        {
            return "No se pueden agregar horas que estén entre otras horas de este mismo día.";
        }
    }
}