using CreditCardAPI.Models;

namespace CreditCardMVC.Models
{
    public class EstadoDeCuentaViewModel
    {
        // Propiedad que representa el estado de cuenta actual utilizando los objetos EstadoCuenta del modelo EstadoCuenta
        public EstadoCuenta EstadoCuenta { get; set; }

        // Lista que contiene el historial de transacciones
        public List<Transacciones> HistorialTransacciones { get; set; }
    }
}
