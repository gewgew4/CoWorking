using CoWorking.Domain.Comun;

namespace CoWorking.Domain.Errores;

public static class Errores
{
    public static class Reserva
    {
        public static readonly Error SalaRequerida = new("Reserva.SalaRequerida", "El código de sala es obligatorio.");
        public static readonly Error DniInválido = new("Reserva.DniInválido", "El DNI debe tener 7 u 8 dígitos numéricos.");
        public static readonly Error DuraciónInválida = new("Reserva.DuraciónInválida", "La duración debe estar entre 1 y 8 horas.");
        public static readonly Error YaCancelada = new("Reserva.YaCancelada", "La reserva ya fue cancelada.");
        public static readonly Error CancelaciónTardía = new("Reserva.CancelaciónTardía", "Solo se puede cancelar con al menos 2 horas de anticipación.");
        public static readonly Error NoEncontrada = new("Reserva.NoEncontrada", "No se encontró la reserva.");
    }
}
