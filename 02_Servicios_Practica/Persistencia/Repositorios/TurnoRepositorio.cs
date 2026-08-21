using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class TurnoRepositorio(IDbConnectionFactory connectionFactory) : ITurnoRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory
        = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Turno> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id,
                   cliente_id AS ClienteId,
                   profesional_id AS ProfesionalId,
                   servicio_id AS ServicioId,
                   fecha_hora AS FechaHora,
                   estado AS Estado
            FROM turnos
            ORDER BY fecha_hora;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var turnos = connection.Query<Turno>(sql);
        return turnos.AsList();
    }

    public Turno? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id,
                   cliente_id AS ClienteId,
                   profesional_id AS ProfesionalId,
                   servicio_id AS ServicioId,
                   fecha_hora AS FechaHora,
                   estado AS Estado
            FROM turnos
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Turno>(sql, new { Id = id });
    }

    public bool ExisteTurno(int profesionalId, DateTime fechaHora)
    {
        const string sql = """
            SELECT COUNT(1)
            FROM turnos
            WHERE profesional_id = @ProfesionalId
              AND fecha_hora = @FechaHora
              AND estado <> 'Cancelado';
            """;

        using var connection = _connectionFactory.CrearConexion();
        var cantidad = connection.ExecuteScalar<int>(
            sql,
            new { ProfesionalId = profesionalId, FechaHora = fechaHora });
        return cantidad > 0;
    }

    public int Crear(Turno turno)
    {
        const string sql = """
            INSERT INTO turnos (cliente_id, profesional_id, servicio_id, fecha_hora, estado)
            VALUES (@ClienteId, @ProfesionalId, @ServicioId, @FechaHora, @Estado);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, turno);
    }

    public bool ActualizarEstado(int id, string estado)
    {
        const string sql = """
            UPDATE turnos
            SET estado = @Estado
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new { Id = id, Estado = estado });
        return filas > 0;
    }
}
