using System;
using System.Collections.Generic;

namespace CreditCardAPI.Models;

public partial class TarjetasDeCredito
{
    public int TarjetaId { get; set; }

    public int? UsuarioId { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public decimal LimiteCredito { get; set; }

    public decimal SaldoActual { get; set; }

    public decimal? SaldoDisponible { get; set; }

    public virtual ICollection<Transacciones> Transacciones { get; set; } = new List<Transacciones>();

    public virtual Usuario? Usuario { get; set; }
}
