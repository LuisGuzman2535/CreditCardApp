using CreditCardAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardMVC.Controllers
{
    public class TarjetasDeCreditoController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public TarjetasDeCreditoController(IHttpClientFactory httpClientFactory)
        {
            // Crea una instancia de HttpClient utilizando la fábrica de clientes HTTP
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }
        public async Task<IActionResult> CreateTarjetaCredito()
        {
            var model = new UsuarioTarjetaDTO();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTarjetaCredito(UsuarioTarjetaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/TarjetasDeCredito/AgregarTarjetaCredito", dto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CreateTarjetaCredito");
            }

            ModelState.AddModelError(string.Empty, "Error al crear el usuario y la tarjeta de crédito.");
            return View("Error", dto);
        }
    }
}
