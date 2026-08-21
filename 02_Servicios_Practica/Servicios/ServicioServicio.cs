using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ServicioServicio(IServicioRepositorio servicioRepositorio)
{
    private readonly IServicioRepositorio _servicioRepositorio =
        servicioRepositorio ?? throw new ArgumentNullException(nameof(servicioRepositorio));

    public IReadOnlyList<Servicio> ObtenerTodos()
    {
        return _servicioRepositorio.ObtenerTodos();
    }

    public Servicio? ObtenerPorId(int id)
    {
        return _servicioRepositorio.ObtenerPorId(id);
    }

    public int Crear(Servicio servicio)
    {
        return _servicioRepositorio.Crear(servicio);
    }
}