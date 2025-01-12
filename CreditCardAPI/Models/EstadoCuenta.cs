namespace CreditCardAPI.Models
{
    public class EstadoCuenta
    {
        // Identificador de la tarjeta de crédito
        public int TarjetaId { get; set; }

        // Número de la tarjeta de crédito
        public string NumeroTarjeta { get; set; }

        // Nombre del titular de la tarjeta
        public string Nombre { get; set; }

        // Saldo actual de la tarjeta
        public decimal SaldoActual { get; set; }

        // Límite de crédito de la tarjeta
        public decimal LimiteCredito { get; set; }

        // Saldo disponible de la tarjeta
        public decimal SaldoDisponible { get; set; }

        // Total de compras realizadas en el mes actual (puede ser nulo)
        public decimal? TotalComprasMesActual { get; set; }

        // Total de compras realizadas en el mes anterior (puede ser nulo)
        public decimal? TotalComprasMesAnterior { get; set; }

        // Interés bonificable de la tarjeta
        public decimal InteresBonificable { get; set; }

        // Cuota mínima a pagar
        public decimal CuotaMinima { get; set; }

        // Monto total a pagar
        public decimal MontoTotal { get; set; }

        // Pago contado con intereses
        public decimal PagoContadoConIntereses { get; set; }

        // Este modelo se utiliza para el procedimiento almacenado 'ObtenerEstadoDeCuenta'
    }
}
