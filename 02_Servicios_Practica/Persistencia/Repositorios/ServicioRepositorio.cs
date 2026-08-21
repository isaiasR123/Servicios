using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class ServicioRepositorio(IDbConnectionFactory connectionFactory) : IServicioRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Servicio> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, duracion_minutos AS DuracionMinutos, precio AS Precio
            FROM servicios
            ORDER BY nombre;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var servicios = connection.Query<Servicio>(sql);
        return servicios.AsList();
    }

    public Servicio? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, duracion_minutos AS DuracionMinutos, precio AS Precio
            FROM servicios
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Servicio>(sql, new { Id = id });
    }

    public int Crear(Servicio servicio)
    {
        const string sql = """
            INSERT INTO servicios (nombre, duracion_minutos, precio)
            VALUES (@Nombre, @DuracionMinutos, @Precio);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, servicio);
    }
}
