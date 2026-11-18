using CoWorking.Domain.Comun;
using CoWorking.Domain.Enums;

namespace CoWorking.Domain.Entidades;

public class ReservaSala
{
    // TODO (Punto 1): Cerrar el constructor y los setters públicos para proteger las invariantes.
    public ReservaSala() { }

    public Guid Id { get; set; }
    public string SalaCódigo { get; set; } = string.Empty;
    public string SocioDni { get; set; } = string.Empty;
    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public decimal Importe { get; set; }
    public EstadoReserva Estado { get; set; }

    // TODO (Punto 1): Validar salaCódigo, socioDni (7 u 8 dígitos) y horas (1 a 8) usando Result.Fallo / Result.Éxito.
    // Calcular Fin e Importe (usando el calculador recibido) e inicializar Estado en Confirmada.
    public static Result<ReservaSala> Crear(
        string salaCódigo,
        string socioDni,
        DateTime inicio,
        int horas,
        ICalculadorTarifa calculador)
    {
        return Result.Éxito(new ReservaSala
        {
            Id = Guid.NewGuid(),
            SalaCódigo = salaCódigo,
            SocioDni = socioDni,
            Inicio = inicio
        });
    }

    // TODO (Punto 1): Implementar Cancelar(DateTime ahora) validando YaCancelada y CancelaciónTardía (< 2 horas).
    public Result Cancelar(DateTime ahora)
    {
        Estado = EstadoReserva.Cancelada;
        return Result.Éxito();
    }
}
