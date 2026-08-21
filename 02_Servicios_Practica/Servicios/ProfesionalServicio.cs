using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ProfesionalServicio(IProfesionalRepositorio profesionalRepositorio)
{
    private readonly IProfesionalRepositorio _profesionalRepositorio =
        profesionalRepositorio ?? throw new ArgumentNullException(nameof(profesionalRepositorio));

    public IReadOnlyList<Profesional> ObtenerTodos()
    {
        return _profesionalRepositorio.ObtenerTodos();
    }

    public Profesional? ObtenerPorId(int id)
    {
        return _profesionalRepositorio.ObtenerPorId(id);
    }

    public int Crear(Profesional profesional)
    {
        return _profesionalRepositorio.Crear(profesional);
    }
}