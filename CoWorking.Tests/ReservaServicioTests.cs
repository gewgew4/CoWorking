using CoWorking.Application.Servicios;
using CoWorking.Domain.Comun;
using CoWorking.Domain.Entidades;
using CoWorking.Domain.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Tests;

public class ReservaServicioTests
{
    sealed class CalculadorDoble : ICalculadorTarifa
    {
        public decimal Calcular(int horas, DateTime inicio) => horas * 10000m;
    }

    sealed class RepoMemoria : IReservaRepositorio
    {
        readonly List<ReservaSala> _items = [];
        public Task AgregarAsync(ReservaSala reserva) { _items.Add(reserva); return Task.CompletedTask; }
        public Task<ReservaSala?> ObtenerPorIdAsync(Guid id) => Task.FromResult(_items.FirstOrDefault(r => r.Id == id));
        public Task<IReadOnlyList<ReservaSala>> ListarAsync() => Task.FromResult<IReadOnlyList<ReservaSala>>(_items);
    }

    [Fact]
    public async Task CrearReservaAsync_UsaCalculadorInyectadoPorDI()
    {
        var servicios = new ServiceCollection();
        servicios.AddSingleton<IReservaRepositorio, RepoMemoria>();
        servicios.AddScoped<ICalculadorTarifa, CalculadorDoble>();
        servicios.AddScoped<ReservaServicio>();

        using var proveedor = servicios.BuildServiceProvider();
        var servicio = proveedor.GetRequiredService<ReservaServicio>();

        var resultado = await servicio.CrearReservaAsync("SALA-B", "35123456", new DateTime(2026, 11, 20, 19, 0, 0), 2);

        Assert.True(resultado.EsÉxito);
        Assert.Equal(20000m, resultado.Valor.Importe);
    }
}
