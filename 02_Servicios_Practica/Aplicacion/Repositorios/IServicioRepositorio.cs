using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IServicioRepositorio
{
    IReadOnlyList<Servicio> ObtenerTodos();

    Servicio? ObtenerPorId(int id);

    int Crear(Servicio servicio);
}
