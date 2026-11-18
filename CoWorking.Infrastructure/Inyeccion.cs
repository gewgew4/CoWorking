using CoWorking.Domain.Repositorios;
using CoWorking.Infrastructure.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Infrastructure;

public static class Inyeccion
{
    public static IServiceCollection AgregarInfraestructura(this IServiceCollection services)
    {
        services.AddSingleton<IReservaRepositorio, MemoriaReservaRepositorio>();
        return services;
    }
}
