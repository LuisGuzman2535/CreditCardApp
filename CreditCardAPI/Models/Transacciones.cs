using System;
using System.Collections.Generic;

namespace CreditCardAPI.Models;

public partial class Transacciones
{
    public int TransaccionId { get; set; }

    public int? TarjetaId { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Descripcion { get; set; }

    public decimal Monto { get; set; }

   public virtual TarjetasDeCredito? Tarjeta { get; set; }
    public string TipoTransaccion { get; set; }
}
