
namespace EitechPfe.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public LicenseRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> Add(License license)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, created_at, last_update_at, is_archived)
                VALUES (@ProductId, @MaxDevices, @Duration, @GracePeriod, @PublicKey, @Price, @CreatedAt, @LastUpdateAt, @IsArchived);";
            return await connection.ExecuteAsync(sql, license);
        }

        public async Task<License?> GetById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM licenses WHERE license_id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<License>(sql, new { Id = id });
        }

        public async Task<IEnumerable<License>> GetAll()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM licenses;";
            return await connection.QueryAsync<License>(sql);
        }

        public async Task<int> Update(License license)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE licenses
                SET product_id = @ProductId, max_devices = @MaxDevices, duration = @Duration, 
                    grace_period = @GracePeriod, public_key = @PublicKey, price = @Price, 
                    last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE license_id = @LicenseId;";
            return await connection.ExecuteAsync(sql, license);
        }

        public async Task<int> Delete(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM licenses WHERE license_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
