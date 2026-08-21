using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IClienteRepositorio
{
    IReadOnlyList<Cliente> ObtenerTodos();

    Cliente? ObtenerPorId(int id);

    int Crear(Cliente cliente);
}
