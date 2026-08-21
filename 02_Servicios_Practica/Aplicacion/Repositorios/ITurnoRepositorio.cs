using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface ITurnoRepositorio
{
    IReadOnlyList<Turno> ObtenerTodos();

    Turno? ObtenerPorId(int id);

    bool ExisteTurno(int profesionalId, DateTime fechaHora);

    int Crear(Turno turno);

    bool ActualizarEstado(int id, string estado);
}
