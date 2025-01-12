using CreditCardAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardMVC.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public TransaccionController(IHttpClientFactory httpClientFactory)
        {
            // Crea una instancia de HttpClient utilizando la fábrica de clientes HTTP
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }

        //Devuele un listado de transacciones dependiente de la tarjeta de credito seleccionada
        [HttpGet]
        public async Task<IActionResult> Transacciones(int id)
        {
            try
            {
                var tarjetaTransaccionesResponse = await _httpClient.GetAsync($"api/TarjetasDeCredito/TarjetaDeCredito/{id}");

                if (tarjetaTransaccionesResponse.IsSuccessStatusCode)
                {
                    var tarjetaTransacciones = await tarjetaTransaccionesResponse.Content.ReadAsAsync<TarjetasDeCredito>();

                    var historial = new TarjetasDeCredito
                    {
                        Usuario = tarjetaTransacciones.Usuario,
                        NumeroTarjeta = tarjetaTransacciones.NumeroTarjeta,
                        Transacciones = tarjetaTransacciones.Transacciones
                            .Where(t => t.Fecha.Month == DateTime.Now.Month)
                            .OrderByDescending(t => t.Fecha)
                            .ToList()
                    };

                    return View(historial);
                }

                ViewBag.ErrorMessage = "No se pudo obtener la información de la tarjeta de crédito.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al obtener las transacciones.";
                return View("Error");
            }
        }
    }
}
