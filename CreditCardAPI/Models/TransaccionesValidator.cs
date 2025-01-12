namespace CreditCardAPI.Models
{
    using FluentValidation;

    public class TransaccionesValidator : AbstractValidator<Transacciones>
    {
        public TransaccionesValidator()
        {
            // Regla para validar que el ID de la tarjeta no sea nulo
            RuleFor(x => x.TarjetaId)
                .NotNull()
                .WithMessage("El ID de la tarjeta es requerido.");

            // Regla para validar que la fecha no esté vacía
            RuleFor(x => x.Fecha)
                .NotEmpty()
                .WithMessage("La fecha es requerida.");

            // Regla para validar que la descripción no esté vacía
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripción es requerida.");

            // Regla para validar que el monto sea mayor a cero
            RuleFor(x => x.Monto)
                .GreaterThan(0)
                .WithMessage("El monto debe ser mayor a cero.");

            // Regla para validar que el tipo de transacción no esté vacío
            RuleFor(x => x.TipoTransaccion)
                .NotEmpty()
                .WithMessage("El tipo de transacción es requerido.");
        }
    }
}
