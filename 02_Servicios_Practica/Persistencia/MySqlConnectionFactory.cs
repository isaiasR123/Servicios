using System.Data.Common;
using MySqlConnector;

namespace Persistencia;

public sealed class MySqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = string.IsNullOrWhiteSpace(connectionString)
        ? throw new ArgumentException("La cadena de conexion no puede estar vacia.", nameof(connectionString))
        : connectionString;

    public DbConnection CrearConexion()
    {
        return new MySqlConnection(_connectionString);
    }
}
