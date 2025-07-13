using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace obligatorioProg3.Models.Cotizacion
{
    public class ConexionAdoCotizacion : DbContext
    {
        public ConexionAdoCotizacion() : base("name=ConexionAdoCotizacion") { } // conexion a la base de datos desde otro connectionstring

        public DbSet<obligatorioProg3.Models.cotizacion> cotizacion { get; set; } // usando la clase de edmx cotizacion indicando
                                                                                  // desde el el modelo q genero entity,
                                                                                  // para q no haya confusion con la clase manual creada CotizacionAdo
    }
}
