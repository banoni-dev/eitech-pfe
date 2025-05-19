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
            string sql = @"
                SELECT 
                    license_id AS LicenseId,
                    product_id AS ProductId,
                    max_devices AS MaxDevices,
                    duration AS Duration,
                    grace_period AS GracePeriod,
                    public_key AS PublicKey,
                    price AS Price,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM licenses
                WHERE license_id = @Id AND is_archived = FALSE;";
            return await connection.QueryFirstOrDefaultAsync<License>(sql, new { Id = id });
        }

        public async Task<IEnumerable<License>> GetAll()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    license_id AS LicenseId,
                    product_id AS ProductId,
                    max_devices AS MaxDevices,
                    duration AS Duration,
                    grace_period AS GracePeriod,
                    public_key AS PublicKey,
                    price AS Price,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM licenses
                WHERE is_archived = FALSE;";
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
            string sql = "UPDATE licenses SET is_archived = TRUE WHERE license_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<LicenseOrder?> GetLicenseOrderByUserAndLicense(int userId, int licenseId)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT * 
                FROM license_orders 
                WHERE user_id = @UserId AND license_id = @LicenseId AND is_archived = FALSE;";
            return await connection.QueryFirstOrDefaultAsync<LicenseOrder>(sql, new { UserId = userId, LicenseId = licenseId });
        }
    }
}
