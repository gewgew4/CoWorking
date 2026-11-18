using CoWorking.Domain.Comun;
using CoWorking.Domain.Enums;

namespace CoWorking.Domain.Entidades;

public class ReservaSala
{
    private ReservaSala() { }

    private ReservaSala(
        string salaCódigo,
        string socioDni,
        DateTime inicio,
        DateTime fin,
        decimal importe)
    {
        Id = Guid.NewGuid();
        SalaCódigo = salaCódigo;
        SocioDni = socioDni;
        Inicio = inicio;
        Fin = fin;
        Importe = importe;
        Estado = EstadoReserva.Confirmada;
    }

    public Guid Id { get; private set; }
    public string SalaCódigo { get; private set; } = string.Empty;
    public string SocioDni { get; private set; } = string.Empty;
    public DateTime Inicio { get; private set; }
    public DateTime Fin { get; private set; }
    public decimal Importe { get; private set; }
    public EstadoReserva Estado { get; private set; }

    public static Result<ReservaSala> Crear(
        string salaCódigo,
        string socioDni,
        DateTime inicio,
        int horas,
        ICalculadorTarifa calculador)
    {
        if (string.IsNullOrWhiteSpace(salaCódigo))
            return Result.Fallo<ReservaSala>(Errores.Errores.Reserva.SalaRequerida);

        if (string.IsNullOrWhiteSpace(socioDni) ||
            socioDni.Length is < 7 or > 8 ||
            !socioDni.All(char.IsDigit))
            return Result.Fallo<ReservaSala>(Errores.Errores.Reserva.DniInválido);

        if (horas is < 1 or > 8)
            return Result.Fallo<ReservaSala>(Errores.Errores.Reserva.DuraciónInválida);

        var fin = inicio.AddHours(horas);
        var importe = calculador.Calcular(horas, inicio);

        return Result.Éxito(new ReservaSala(salaCódigo.Trim(), socioDni, inicio, fin, importe));
    }

    public Result Cancelar(DateTime ahora)
    {
        if (Estado == EstadoReserva.Cancelada)
            return Result.Fallo(Errores.Errores.Reserva.YaCancelada);

        if ((Inicio - ahora).TotalHours < 2)
            return Result.Fallo(Errores.Errores.Reserva.CancelaciónTardía);

        Estado = EstadoReserva.Cancelada;
        return Result.Éxito();
    }
}
