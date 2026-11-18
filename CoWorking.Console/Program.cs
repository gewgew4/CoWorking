using System.Text;
using CoWorking.Application.Servicios;
using CoWorking.Domain.Comun;
using CoWorking.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Console;

internal class Program
{
    static async Task Main()
    {
        System.Console.OutputEncoding = Encoding.UTF8;

        var servicios = new ServiceCollection();
        servicios.AgregarInfraestructura();

        // TODO (Punto 3): Registrar ICalculadorTarifa (con la implementación de horario pico) y ReservaServicio en DI.

        using var proveedor = servicios.BuildServiceProvider();
        var servicio = proveedor.GetRequiredService<ReservaServicio>();

        var resultado = await servicio.CrearReservaAsync("SALA-A", "35123456", new DateTime(2026, 11, 20, 19, 0, 0), 2);
        if (resultado.EsFallo)
        {
            System.Console.WriteLine($"Error [{resultado.Error.Código}]: {resultado.Error.Mensaje}");
            return;
        }

        var reserva = resultado.Valor;
        System.Console.WriteLine($"Reserva creada: {reserva.SalaCódigo} | {reserva.Inicio:HH:mm}-{reserva.Fin:HH:mm} | ${reserva.Importe:N2}");
    }
}
