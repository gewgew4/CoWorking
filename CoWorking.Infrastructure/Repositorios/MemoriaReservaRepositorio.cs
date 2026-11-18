using CoWorking.Domain.Entidades;
using CoWorking.Domain.Repositorios;

namespace CoWorking.Infrastructure.Repositorios;

public sealed class MemoriaReservaRepositorio : IReservaRepositorio
{
    readonly List<ReservaSala> _reservas = [];

    public Task AgregarAsync(ReservaSala reserva)
    {
        _reservas.Add(reserva);
        return Task.CompletedTask;
    }

    public Task<ReservaSala?> ObtenerPorIdAsync(Guid id) =>
        Task.FromResult(_reservas.FirstOrDefault(r => r.Id == id));

    public Task<IReadOnlyList<ReservaSala>> ListarAsync() =>
        Task.FromResult<IReadOnlyList<ReservaSala>>(_reservas.ToList());
}
