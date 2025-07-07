using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace obligatorioProg3.CustomValidations
{
    public class ValidacionCedulaUruguaya : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string cedula = value.ToString();

            cedula = cedula.Trim();
            cedula = cedula.Replace(".", "");
            cedula = cedula.Replace("-", "");

            if (cedula.Length != 8)
            {
                return false;
            }

            int[] constante = { 2, 9, 8, 7, 6, 3, 4 };

            string numSinVerificador = cedula.Substring(0, 7);
            int digitoVerificador = Convert.ToInt32(cedula.Substring(7, 1));

            int suma = 0;

            for (int i = 0; i < 7; i++)
            {
                suma += (Convert.ToInt32(numSinVerificador[i].ToString()) * constante[i]);
            }

            int digitoCalculado = 10 - (suma % 10);
            if (digitoCalculado == 10) digitoCalculado = 0;

            return digitoVerificador == digitoCalculado;
        }

        public override string FormatErrorMessage(string name)
        {
            return "La cédula ingresada no es válida.";
        }
    }
}