using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obligatorioProg3.CustomValidations
{
    public static class Utilidades
    {
        public static bool usuarioTieneAccesoACrudSiONo(HttpSessionStateBase session)
        {
            if (session["IdRol"] == null)
                return false;

            int idRol = (int)session["IdRol"];
            return idRol == 2 || idRol == 3;
        }
    }

}