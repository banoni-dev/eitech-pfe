using Dapper;
using System.Data;
using EitechPfe.Repositories.Interfaces;

public class BlackListedRepository : IBlackListedRepository
{
    private readonly DatabaseConfig _dbConfig;

    public BlackListedRepository(DatabaseConfig dbConfig)
    {
        _dbConfig = dbConfig;
    }

    public async Task<int> CreateBlackListed(BlackListed blackListed)
    {
        using var connection = _dbConfig.GetConnection();
        string sql = @"
            INSERT INTO black_listed (ip, type, blocked_date, recovery_date, created_at, updated_at)
            VALUES (@Ip, @Type, @BlockedDate, @RecoveryDate, @CreatedAt, @UpdatedAt);";
        return await connection.ExecuteAsync(sql, blackListed);
    }

    public async Task<BlackListed?> GetBlackListedById(int id)
    {
        using var connection = _dbConfig.GetConnection();
        string sql = "SELECT * FROM black_listed WHERE id = @Id;";
        return await connection.QueryFirstOrDefaultAsync<BlackListed>(sql, new { Id = id });
    }

    public async Task<IEnumerable<BlackListed>> GetAllBlackListed()
    {
        using var connection = _dbConfig.GetConnection();
        string sql = "SELECT * FROM black_listed;";
        return await connection.QueryAsync<BlackListed>(sql);
    }

    public async Task<int> UpdateBlackListed(BlackListed blackListed)
    {
        using var connection = _dbConfig.GetConnection();
        string sql = @"
            UPDATE black_listed
            SET ip = @Ip, type = @Type, blocked_date = @BlockedDate, recovery_date = @RecoveryDate, 
                updated_at = @UpdatedAt
            WHERE id = @Id;";
        return await connection.ExecuteAsync(sql, blackListed);
    }

    public async Task<int> DeleteBlackListed(int id)
    {
        using var connection = _dbConfig.GetConnection();
        string sql = "DELETE FROM black_listed WHERE id = @Id;";
        return await connection.ExecuteAsync(sql, new { Id = id });
    }
}
