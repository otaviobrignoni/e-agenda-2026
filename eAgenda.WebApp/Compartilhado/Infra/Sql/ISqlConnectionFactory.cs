using Microsoft.Data.SqlClient;

namespace eAgenda.WebApp.Compartilhado.Infra.Sql;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
