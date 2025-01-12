using CreditCardAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoDeCuentaController : ControllerBase
    {
        private readonly CreditCardDbContext _context;
        public EstadoDeCuentaController(CreditCardDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetEstadoCuenta/{id:int}")]
        [SwaggerOperation(Summary = "Obtiene el estado de cuenta para una tarjeta de crédito")]
        [SwaggerResponse(200, "Estado de cuenta obtenido correctamente", typeof(EstadoCuenta))]
        [SwaggerResponse(404, "No se encontró el estado de cuenta")]
        public async Task<IActionResult> GetEstadoCuenta(int id)
        {
            var estadoCuenta = await _context.ObtenerEstadoCuentaAsync(id);

            if (estadoCuenta == null)
            {
                return NotFound();
            }

            return Ok(estadoCuenta);
        }

        [HttpGet]
        [Route("GetHistorialTransacciones/{id:int}")]
        [SwaggerOperation(Summary = "Obtiene el historial de transacciones para una tarjeta de crédito.")]
        [SwaggerResponse(200, "Historial de transacciones obtenido correctamente", typeof(List<Transacciones>))]
        public async Task<List<Transacciones>> GetHistorialTransaccionesAsync(int id)
        {
            var historialTransacciones = await _context.Transacciones
                .FromSqlRaw("EXEC ObtenerHistorialTransacciones @TarjetaID = {0}", id)
                .ToListAsync();

            return historialTransacciones;
        }

    }
}
