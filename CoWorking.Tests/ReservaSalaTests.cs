using CoWorking.Domain.Comun;
using CoWorking.Domain.Entidades;
using CoWorking.Domain.Enums;
using CoWorking.Domain.Errores;

namespace CoWorking.Tests;

public class ReservaSalaTests
{
    readonly ICalculadorTarifa _calculador = new CalculadorTarifaBase();

    [Fact]
    public void Crear_ConDatosValidos_CalculaFinImporteYEstadoConfirmada()
    {
        var inicio = new DateTime(2026, 11, 20, 14, 0, 0);

        var resultado = ReservaSala.Crear("SALA-1", "35123456", inicio, 3, _calculador);

        Assert.True(resultado.EsÉxito);
        Assert.Equal(inicio.AddHours(3), resultado.Valor.Fin);
        Assert.Equal(15000m, resultado.Valor.Importe);
        Assert.Equal(EstadoReserva.Confirmada, resultado.Valor.Estado);
    }

    [Theory]
    [InlineData("", "35123456", 2, "Reserva.SalaRequerida")]
    [InlineData("SALA-1", "123", 2, "Reserva.DniInválido")]
    [InlineData("SALA-1", "3512345A", 2, "Reserva.DniInválido")]
    [InlineData("SALA-1", "35123456", 0, "Reserva.DuraciónInválida")]
    [InlineData("SALA-1", "35123456", 9, "Reserva.DuraciónInválida")]
    public void Crear_ConDatosInvalidos_DevuelveFalloEsperado(
        string sala, string dni, int horas, string códigoEsperado)
    {
        var inicio = new DateTime(2026, 11, 20, 14, 0, 0);

        var resultado = ReservaSala.Crear(sala, dni, inicio, horas, _calculador);

        Assert.True(resultado.EsFallo);
        Assert.Equal(códigoEsperado, resultado.Error.Código);
    }

    [Fact]
    public void Cancelar_ConMenosDeDosHoras_DevuelveCancelacionTardia()
    {
        var inicio = new DateTime(2026, 11, 20, 16, 0, 0);
        var reserva = ReservaSala.Crear("SALA-1", "35123456", inicio, 2, _calculador).Valor;

        var resultado = reserva.Cancelar(inicio.AddHours(-1));

        Assert.True(resultado.EsFallo);
        Assert.Equal(Errores.Reserva.CancelaciónTardía, resultado.Error);
        Assert.Equal(EstadoReserva.Confirmada, reserva.Estado);
    }

    [Fact]
    public void Cancelar_DosVeces_DevuelveYaCancelada()
    {
        var inicio = new DateTime(2026, 11, 20, 16, 0, 0);
        var reserva = ReservaSala.Crear("SALA-1", "35123456", inicio, 2, _calculador).Valor;

        var primero = reserva.Cancelar(inicio.AddHours(-4));
        var segundo = reserva.Cancelar(inicio.AddHours(-3));

        Assert.True(primero.EsÉxito);
        Assert.Equal(EstadoReserva.Cancelada, reserva.Estado);
        Assert.True(segundo.EsFallo);
        Assert.Equal(Errores.Reserva.YaCancelada, segundo.Error);
    }

    [Fact]
    public void ReservaSala_NoDebeTenerConstructoresNiSettersPublicos()
    {
        var tipo = typeof(ReservaSala);

        Assert.Empty(tipo.GetConstructors());
        Assert.All(tipo.GetProperties(), p =>
            Assert.True(p.SetMethod is null || !p.SetMethod.IsPublic,
                $"La propiedad {p.Name} no debería tener setter público."));
    }
}
