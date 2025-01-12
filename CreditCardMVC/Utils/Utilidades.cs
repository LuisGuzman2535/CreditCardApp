namespace CreditCardMVC.Utils
{
    public class Utilidades
    {
        public static string OcultarNumeroTarjeta(string numeroTarjeta)
        {
            if (numeroTarjeta.Length == 16)
            {
                return new string('*', 12) + numeroTarjeta.Substring(12, 4);
            }
            return numeroTarjeta;
        }
    }
}
