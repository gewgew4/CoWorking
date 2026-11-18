namespace CoWorking.Domain.Comun;

public sealed class CalculadorTarifaBase : ICalculadorTarifa
{
    const decimal TarifaHora = 5000m;

    public decimal Calcular(int horas, DateTime inicio) => horas * TarifaHora;
}
