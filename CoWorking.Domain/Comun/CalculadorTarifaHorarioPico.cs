namespace CoWorking.Domain.Comun;

public sealed class CalculadorTarifaHorarioPico : ICalculadorTarifa
{
    const decimal TarifaHora = 5000m;
    const decimal RecargoPico = 1.25m;

    public decimal Calcular(int horas, DateTime inicio)
    {
        var baseImporte = horas * TarifaHora;
        return inicio.Hour >= 18 ? baseImporte * RecargoPico : baseImporte;
    }
}
