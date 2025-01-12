﻿using CreditCardAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasDeCreditoController : ControllerBase
    {
        private readonly CreditCardDbContext _context; 

        public TarjetasDeCreditoController(CreditCardDbContext creditCardDbContext)
        {
            _context = creditCardDbContext; 
        }

        [HttpGet]
        [Route("TarjetaDeCredito/{id:int}")]
        [SwaggerOperation(Summary = "Obtiene una tarjeta de crédito específica por su ID.")]
        [SwaggerResponse(200, "Tarjeta de crédito obtenida correctamente", typeof(TarjetasDeCredito))]
        [SwaggerResponse(404, "No se encontró la tarjeta de crédito")]
        public async Task<ActionResult<TarjetasDeCredito>> GetTarjetaDecredito(int id)
        {
            var tarjetaCredito = await _context.TarjetasDeCreditos
                .Where(t => t.TarjetaId == id)
                .Include(t => t.Transacciones)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync();

            if (tarjetaCredito == null)
            {
                return NotFound();
            }

            return Ok(tarjetaCredito);
        }

        [HttpGet]
        [Route("ListadoDeTarjetas")]
        [SwaggerOperation(Summary = "Obtiene un listado de todas las tarjetas de crédito.")]
        [SwaggerResponse(200, "Listado de tarjetas de crédito obtenido correctamente", typeof(List<TarjetasDeCredito>))]
        public async Task<ActionResult<List<TarjetasDeCredito>>> GetListadoDeTarjetas()
        {
            var listadoTarjetas = await _context.TarjetasDeCreditos
                .Select(t => new TarjetasDeCredito
                {
                    TarjetaId = t.TarjetaId,
                    NumeroTarjeta = t.NumeroTarjeta,
                    Usuario = new Usuario { Nombre = t.Usuario.Nombre }
                })
                .ToListAsync();

            return Ok(listadoTarjetas);
        }
    }
}
