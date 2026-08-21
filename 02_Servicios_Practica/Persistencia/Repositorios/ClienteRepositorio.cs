using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class ClienteRepositorio(IDbConnectionFactory connectionFactory) : IClienteRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Cliente> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, telefono AS Telefono, email AS Email
            FROM clientes
            ORDER BY apellido, nombre;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var clientes = connection.Query<Cliente>(sql);
        return clientes.AsList();
    }

    public Cliente? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, apellido AS Apellido, telefono AS Telefono, email AS Email
            FROM clientes
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Cliente>(sql, new { Id = id });
    }

    public int Crear(Cliente cliente)
    {
        const string sql = """
            INSERT INTO clientes (nombre, apellido, telefono, email)
            VALUES (@Nombre, @Apellido, @Telefono, @Email);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, cliente);
    }
}
