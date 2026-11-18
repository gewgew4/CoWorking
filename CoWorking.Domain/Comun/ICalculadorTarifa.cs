namespace CoWorking.Domain.Comun;

public interface ICalculadorTarifa
{
    decimal Calcular(int horas, DateTime inicio);
}
