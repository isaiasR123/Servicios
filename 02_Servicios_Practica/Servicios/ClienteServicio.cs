using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ClienteServicio(IClienteRepositorio clienteRepositorio)
{
    private readonly IClienteRepositorio _clienteRepositorio =
        clienteRepositorio ?? throw new ArgumentNullException(nameof(clienteRepositorio));

    public IReadOnlyList<Cliente> ObtenerTodos()
    {
        return _clienteRepositorio.ObtenerTodos();
    }

    public Cliente? ObtenerPorId(int id)
    {
        return _clienteRepositorio.ObtenerPorId(id);
    }

    public int Crear(Cliente cliente)
    {
        return _clienteRepositorio.Crear(cliente);
    }
}