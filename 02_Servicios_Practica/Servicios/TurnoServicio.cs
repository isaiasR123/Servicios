using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class TurnoServicio(ITurnoRepositorio turnoRepositorio)
{
    private readonly ITurnoRepositorio _turnoRepositorio =
        turnoRepositorio ?? throw new ArgumentNullException(nameof(turnoRepositorio));

    public IReadOnlyList<Turno> ObtenerTodos()
    {
        return _turnoRepositorio.ObtenerTodos();
    }

    public Turno? ObtenerPorId(int id)
    {
        return _turnoRepositorio.ObtenerPorId(id);
    }

    public bool ExisteTurno(int profesionalId, DateTime fechaHora)
    {
        return _turnoRepositorio.ExisteTurno(profesionalId, fechaHora);
    }

    public int Crear(Turno turno)
    {
        return _turnoRepositorio.Crear(turno);
    }

    public bool ActualizarEstado(int id, string estado)
    {
        return _turnoRepositorio.ActualizarEstado(id, estado);
    }
}