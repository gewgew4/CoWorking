# Ejercicio 1: Dominio, Result Pattern y Strategy con DI

## Contexto

La solución `CoWorking` administra reservas de salas por hora. Actualmente compila, pero tiene los problemas más comunes detectados en el parcial: el dominio expone constructores y setters públicos, no valida ni calcula sus propias invariantes, y el servicio instancia la estrategia de tarifa con `new` en lugar de recibirla por inyección de dependencias.

## Qué tenés que resolver

1. **Encapsulamiento y Result Pattern en `ReservaSala`** (`CoWorking.Domain/Entidades/ReservaSala.cs`):
   - Cerrá el constructor y todos los setters públicos de la entidad.
   - Completá `Crear(...)` validando con `Result.Fallo` y los errores de `Errores.Reserva`:
     - `SalaCódigo` no puede estar vacío (`SalaRequerida`).
     - `SocioDni` debe tener exactamente 7 u 8 dígitos numéricos (`DniInválido`).
     - `horas` debe estar entre 1 y 8 (`DuraciónInválida`).
   - Calculá `Fin` (`inicio.AddHours(horas)`), `Importe` (usando el `ICalculadorTarifa` recibido) e inicializá `Estado` en `EstadoReserva.Confirmada`.
   - Completá `Cancelar(DateTime ahora)` validando `YaCancelada` y `CancelaciónTardía` (menos de 2 horas antes de `Inicio`).

2. **Strategy e Inyección de Dependencias** (`CoWorking.Domain` y `CoWorking.Application`):
   - Creá `CalculadorTarifaHorarioPico : ICalculadorTarifa` en `CoWorking.Domain/Comun`: cobra `$5000` por hora, con un `25%` de recargo si `inicio.Hour >= 18`.
   - Modificá `ReservaServicio` para recibir `ICalculadorTarifa` por constructor en lugar de hacer `new CalculadorTarifaBase()`.

3. **Registro en DI** (`CoWorking.Console/Program.cs`):
   - Registrá `ICalculadorTarifa` (con `CalculadorTarifaHorarioPico`) y `ReservaServicio` en `ServiceCollection` para que la consola ejecute sin errores.

## Cómo verificar

Ejecutá las pruebas y la consola:

```powershell
dotnet test
dotnet run --project CoWorking.Console
```
