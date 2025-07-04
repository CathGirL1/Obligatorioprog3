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
        public override bool IsValid(object value)
        {
            var horarioPrograma = HttpContext.Current?.Request;

            string diaElegido = horarioPrograma["dia"];
            string idProgramaStr = horarioPrograma["idPrograma"];

            if (value == null || string.IsNullOrEmpty(diaElegido))  return false;

            TimeSpan horaElegida = (TimeSpan)value;
            int idPrograma = 0;

            if (!string.IsNullOrEmpty(idProgramaStr)) 
            {
                idPrograma = int.Parse(idProgramaStr);
            }

            using (var db = new vozDelEsteBsdEntities())
            {
                var horariosExistentesDiaElegido = db.horario_programa.Where(h => h.dia == diaElegido && h.idPrograma != idPrograma).ToList(); //para evitar errores al editar un horario de un programa que ya existe, se excluye el id del programa que se está editando

                foreach (var horario in horariosExistentesDiaElegido)
                {
                    if (horaElegida > horario.horaInicio && horaElegida < horario.horaFinal)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "No se pueden agregar horas que estén entre otras horas de este mismo día";
        }
    }
}