namespace Catalog.Infrastructure.Core.Sql;

public interface ISqlFactory
{
    SqlConnection CreateConnection(string connectionString);
}
