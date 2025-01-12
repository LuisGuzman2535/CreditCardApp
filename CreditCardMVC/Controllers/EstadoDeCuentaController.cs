using CreditCardAPI.Models;
using CreditCardMVC.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace CreditCardMVC.Controllers
{
    public class EstadoDeCuentaController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public EstadoDeCuentaController(IHttpClientFactory httpClientFactory)
        {
            // Crea una instancia de HttpClient utilizando la fábrica de clientes HTTP
            _httpClient = httpClientFactory.CreateClient("CreditCardAPI");
        }

        //Devuelve el estado de cuenta con sus compras de la tarjeta de credito seleccionada
        [HttpGet]
        public async Task<IActionResult> EstadoDeCuenta(int id)
        {
            try
            {
                var estadoCuentaResponse = await _httpClient.GetAsync($"api/EstadoDeCuenta/GetEstadoCuenta/{id}");
                var transaccionesResponse = await _httpClient.GetAsync($"api/EstadoDeCuenta/GetHistorialTransacciones/{id}");

                if (estadoCuentaResponse.IsSuccessStatusCode && transaccionesResponse.IsSuccessStatusCode)
                {
                    // Lee el contenido de las respuestas HTTP y deserializa a los objetos correspondientes
                    var estadoCuenta = await estadoCuentaResponse.Content.ReadAsAsync<EstadoCuenta>();
                    var historialTransacciones = await transaccionesResponse.Content.ReadAsAsync<List<Transacciones>>();

                    // Crea el ViewModel con los datos obtenidos, filtrando las transacciones por tipo "Compra"
                    var viewModel = new EstadoDeCuentaViewModel
                    {
                        EstadoCuenta = estadoCuenta,
                        HistorialTransacciones = historialTransacciones.Where(h => h.TipoTransaccion == "Compra").ToList()
                    };
                    return View(viewModel);
                }

                ViewBag.ErrorMessage = "No se pudo obtener el estado de cuenta o el historial de transacciones.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al obtener el estado de cuenta.";
                return View("Error");
            }
        }
        //Metodo para exportar el listado de compras realizadas con la tarjeta de credito
        public async Task<IActionResult> ExportToExcel(int id)
        {
            try
            {
                var estadoCuentaResponse = await _httpClient.GetAsync($"api/EstadoDeCuenta/GetEstadoCuenta/{id}");
                var transaccionesResponse = await _httpClient.GetAsync($"api/EstadoDeCuenta/GetHistorialTransacciones/{id}");

                if (estadoCuentaResponse.IsSuccessStatusCode && transaccionesResponse.IsSuccessStatusCode)
                {
                    // Lee el contenido de las respuestas HTTP y deserializa a los objetos correspondientes
                    var estadoCuenta = await estadoCuentaResponse.Content.ReadAsAsync<EstadoCuenta>();
                    var historialTransacciones = await transaccionesResponse.Content.ReadAsAsync<List<Transacciones>>();

                    // Crea el ViewModel con los datos obtenidos, filtrando las transacciones por tipo "Compra"
                    var viewModel = new EstadoDeCuentaViewModel
                    {
                        EstadoCuenta = estadoCuenta,
                        HistorialTransacciones = historialTransacciones.Where(h => h.TipoTransaccion == "Compra").ToList()
                    };

                    // Crea un MemoryStream para el archivo Excel
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        // Crea una hoja de cálculo y carga los datos del historial de transacciones
                        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                        workSheet.TabColor = System.Drawing.Color.Black;
                        workSheet.DefaultRowHeight = 12;
                        workSheet.Cells.LoadFromCollection(historialTransacciones, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    string excelName = "ComprasRecord.xlsx";
                    // Devuelve el archivo Excel generado
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }

                // Si alguna solicitud falla, muestra un mensaje de error
                ViewBag.ErrorMessage = "No se pudo obtener el estado de cuenta o el historial de transacciones.";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al exportar a Excel.";
                return View("Error");
            }
        }
    }
}
