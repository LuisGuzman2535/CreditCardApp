using CreditCardAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly CreditCardDbContext _context;

        public TransaccionController(CreditCardDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddTransaccion")]
        [SwaggerOperation(Summary = "Agrega una nueva transacción.")]
        [SwaggerResponse(200, "Transacción agregada correctamente")]
        [SwaggerResponse(400, "Solicitud incorrecta, datos de la transacción no válidos")]
        public async Task<IActionResult> AgregarTransaccion([FromBody] Transacciones transacciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AgregarTransaccionAsync(transacciones);

            return Ok();
        }

    }
}
