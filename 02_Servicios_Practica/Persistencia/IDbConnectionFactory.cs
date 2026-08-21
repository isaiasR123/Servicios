using System.Data.Common;

namespace Persistencia;

public interface IDbConnectionFactory
{
    DbConnection CrearConexion();
}
