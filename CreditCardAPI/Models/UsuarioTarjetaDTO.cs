namespace CreditCardAPI.Models
{
    public class UsuarioTarjetaDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoActual { get; set; }
    }
}
