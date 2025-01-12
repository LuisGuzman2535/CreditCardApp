using CreditCardAPI.Models;
using CreditCardMVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardMVC.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public PagosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }


        //Este metodo devuelve a la vista un modelo compuesto por información de la tarjeta y transacciones
        [HttpGet]
        public async Task<IActionResult> RegistrarPago(int id)
        {
            try
            {
                var tarjetaResponse = await _httpClient.GetAsync($"api/TarjetasDeCredito/TarjetaDeCredito/{id}");

                if (!tarjetaResponse.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "No se pudo obtener la información de la tarjeta de crédito.";
                    return View("Error");
                }

                var tarjeta = await tarjetaResponse.Content.ReadAsAsync<TarjetasDeCredito>();

                var model = new Transacciones
                {
                    TarjetaId = id,
                    Tarjeta = tarjeta,
                    Fecha = DateOnly.FromDateTime(DateTime.Now)
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al registrar el pago.";
                return View("Error");
            }
        }

        //Agrega un registo de compra del tipo "Pago"
        [HttpPost]
        public async Task<IActionResult> RegistrarPago(Transacciones transaccion)
        {
            try
            {
                //El tipoC representa los Pagos provenientes de una clase static
                transaccion.TipoTransaccion = TiposTransaccion.tipoP;

                var response = await _httpClient.PostAsJsonAsync("api/Transaccion/AddTransaccion", transaccion);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegistrarPago", new { id = transaccion.TarjetaId });
                }

                ViewBag.ErrorMessage = "No se pudo registrar la transacción.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al registrar la transacción.";
                return View("Error");
            }
        }
    }
}
