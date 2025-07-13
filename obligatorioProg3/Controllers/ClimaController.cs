using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using obligatorioProg3.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using obligatorioProg3;

namespace obligatorioProg3.Controllers
{
    public class ClimaController : Controller
    {
        // GET: Clima  
        public async Task<ActionResult> Index()
        {
            string url = "https://api.openweathermap.org/data/2.5/forecast?lat=-34.9&lon=-54.95&appid=12a3592ad8edcaf303644d8568f11054&units=metric&lang=es";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo obtener el clima.";
                    return View();
                }

                var json = await response.Content.ReadAsStringAsync();
                var clima = JsonConvert.DeserializeObject<ClimaAdo>(json);

                var climaActual = clima.List.FirstOrDefault();

                var fecha = climaActual.DtTxt;
                var temperatura = climaActual.Main.Temp;
                var descripcion = climaActual.Weather.FirstOrDefault()?.Description;
                var icono = climaActual.Weather.FirstOrDefault()?.Icon;

                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionAdo"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryVerificarDuplicados = "SELECT COUNT(*) FROM Clima WHERE Fecha = @Fecha"; // Verifica si ya existe un registro con la misma fecha
                    string queryInsertar = "INSERT INTO Clima (Fecha, Temperatura, Descripcion, Icono) VALUES (@Fecha, @Temperatura, @Descripcion, @Icono)";

                    SqlCommand commandVerificarDuplicados = new SqlCommand(queryVerificarDuplicados, connection);
                    commandVerificarDuplicados.Parameters.AddWithValue("@Fecha", fecha);

                    SqlCommand commandInsertar = new SqlCommand(queryInsertar, connection);
                    commandInsertar.Parameters.AddWithValue("@Fecha", fecha);
                    commandInsertar.Parameters.AddWithValue("@Temperatura", temperatura);
                    commandInsertar.Parameters.AddWithValue("@Descripcion", descripcion);
                    commandInsertar.Parameters.AddWithValue("@Icono", icono);

                    var cantidadDuplicados = (int)commandVerificarDuplicados.ExecuteScalar(); //se guarda en la variable la cantidad de climas registrados con la fecha actual

                    if (cantidadDuplicados == 0) //si no hay ningun registro con la fecha actual, se inserta el nuevo clima, si no, no se puede inserta nada porque el dato se repetiría
                    {
                        commandInsertar.ExecuteNonQuery();
                    }
                }

                return View(clima);
            }
        }
    }
}