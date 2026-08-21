using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class ProfesionalRepositorio(IDbConnectionFactory connectionFactory) : IProfesionalRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Profesional> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, especialidad AS Especialidad
            FROM profesionales
            ORDER BY nombre;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var profesionales = connection.Query<Profesional>(sql);
        return profesionales.AsList();
    }

    public Profesional? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, especialidad AS Especialidad
            FROM profesionales
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Profesional>(sql, new { Id = id });
    }

    public int Crear(Profesional profesional)
    {
        const string sql = """
            INSERT INTO profesionales (nombre, especialidad)
            VALUES (@Nombre, @Especialidad);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, profesional);
    }
}
