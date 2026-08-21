using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IProfesionalRepositorio
{
    IReadOnlyList<Profesional> ObtenerTodos();

    Profesional? ObtenerPorId(int id);

    int Crear(Profesional profesional);
}
