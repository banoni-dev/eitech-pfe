using MySql.Data.MySqlClient;
using System.Data;

public class DatabaseConfig
{
    private readonly string _connectionString;

    public DatabaseConfig(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MariaDbConnection")!;
    }

    public IDbConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
