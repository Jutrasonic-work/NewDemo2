namespace Catalog.Infrastructure.Core.Sql;

public class SqlFactory : ISqlFactory
{
    public SqlConnection CreateConnection(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}

