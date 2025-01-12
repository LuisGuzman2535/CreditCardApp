using Azure;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using CreditCardAPI.Models;
using CreditCardMVC.Utils;

namespace CreditCardMVC.Controllers
{
    public class ComprasController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public ComprasController(IHttpClientFactory httpClientFactory)
        {
            // Crea una instancia de HttpClient utilizando la fábrica de clientes HTTP
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }

        //Este metodo devuelve a la vista un modelo compuesto por información de la tarjeta y transacciones
        [HttpGet]
        public async Task<IActionResult> RegistrarCompra(int id)
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
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al registrar la compra.";
                return View("Error");
            }
        }
        //Agrega un registo de compra del tipo "Compra"
        [HttpPost]
        public async Task<IActionResult> RegistrarCompra(Transacciones transaccion)
        {
            try
            {
                //El tipoC representa las Compras provenientes de una clase static
                transaccion.TipoTransaccion = TiposTransaccion.tipoC;

                var response = await _httpClient.PostAsJsonAsync("api/Transaccion/AddTransaccion", transaccion);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegistrarCompra", new { id = transaccion.TarjetaId });
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
