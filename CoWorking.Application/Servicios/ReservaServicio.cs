using CoWorking.Domain.Comun;
using CoWorking.Domain.Entidades;
using CoWorking.Domain.Repositorios;

namespace CoWorking.Application.Servicios;

public sealed class ReservaServicio(IReservaRepositorio repositorio, ICalculadorTarifa calculador)
{
    public async Task<Result<ReservaSala>> CrearReservaAsync(
        string salaCódigo,
        string socioDni,
        DateTime inicio,
        int horas)
    {
        var resultado = ReservaSala.Crear(salaCódigo, socioDni, inicio, horas, calculador);
        if (resultado.EsFallo)
            return resultado;

        await repositorio.AgregarAsync(resultado.Valor);
        return resultado;
    }

    public async Task<Result> CancelarReservaAsync(Guid id, DateTime ahora)
    {
        var reserva = await repositorio.ObtenerPorIdAsync(id);
        if (reserva is null)
            return Result.Fallo(Domain.Errores.Errores.Reserva.NoEncontrada);

        return reserva.Cancelar(ahora);
    }

    public Task<IReadOnlyList<ReservaSala>> ListarAsync() => repositorio.ListarAsync();
}
