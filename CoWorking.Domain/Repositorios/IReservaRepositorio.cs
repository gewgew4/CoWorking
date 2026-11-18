using CoWorking.Domain.Entidades;

namespace CoWorking.Domain.Repositorios;

public interface IReservaRepositorio
{
    Task AgregarAsync(ReservaSala reserva);
    Task<ReservaSala?> ObtenerPorIdAsync(Guid id);
    Task<IReadOnlyList<ReservaSala>> ListarAsync();
}
