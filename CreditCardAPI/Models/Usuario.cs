using System;
using System.Collections.Generic;

namespace CreditCardAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public virtual ICollection<TarjetasDeCredito> TarjetasDeCreditos { get; set; } = new List<TarjetasDeCredito>();
}
