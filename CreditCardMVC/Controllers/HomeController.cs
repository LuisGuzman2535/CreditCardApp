using CreditCardAPI.Models;
using CreditCardMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CreditCardMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            // Crea una instancia de HttpClient utilizando la fábrica de clientes HTTP
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }

        //Devuelve un listado de Tarjetas de Credito
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var listadoTarjetasResponse = await _httpClient.GetAsync("api/TarjetasDeCredito/ListadoDeTarjetas");

                if (listadoTarjetasResponse.IsSuccessStatusCode)
                {
                    var listadoTarjetas = await listadoTarjetasResponse.Content.ReadAsAsync<List<TarjetasDeCredito>>();
                    return View(listadoTarjetas);
                }

                ViewBag.ErrorMessage = "No se pudo obtener el listado de tarjetas de crédito.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al obtener el listado de tarjetas de crédito.";
                return View("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
