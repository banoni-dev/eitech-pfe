namespace EitechPfe.Repositories
{
    public class LicenseBundleRepository : ILicenseBundleRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public LicenseBundleRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> Add(LicenseBundle licenseBundle)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO license_bundles (name, description, price, is_active, created_at, last_update_at)
                VALUES (@Name, @Description, @Price, @IsActive, @CreatedAt, @LastUpdateAt);
                SELECT LAST_INSERT_ID();";
            return await connection.ExecuteScalarAsync<int>(sql, licenseBundle);
        }

        public async Task<LicenseBundle?> GetById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_bundles WHERE license_order_id = @Id AND is_archived = FALSE;";
            return await connection.QueryFirstOrDefaultAsync<LicenseBundle>(sql, new { Id = id });
        }

        public async Task<IEnumerable<LicenseBundle>> GetAll()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_bundles WHERE is_archived = FALSE;";
            return await connection.QueryAsync<LicenseBundle>(sql);
        }

        public async Task<int> Update(LicenseBundle licenseBundle)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE license_bundles
                SET name = @Name, description = @Description, price = @Price,
                    is_active = @IsActive, last_update_at = @LastUpdateAt
                WHERE license_order_id = @LicenseOrderId;";
            return await connection.ExecuteAsync(sql, licenseBundle);
        }

        public async Task<int> Delete(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "UPDATE license_bundles SET is_archived = TRUE WHERE license_order_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
