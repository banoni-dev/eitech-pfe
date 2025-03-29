
namespace EitechPfe.Repositories
{
    public class LicenseOptionRepository : ILicenseOptionRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public LicenseOptionRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateAsync(LicenseOption licenseOption)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO license_options (name, description, price, duration_days, is_active, created_at, last_update_at)
                VALUES (@Name, @Description, @Price, @DurationDays, @IsActive, @CreatedAt, @LastUpdateAt);
                SELECT LAST_INSERT_ID();";
            return await connection.ExecuteScalarAsync<int>(sql, licenseOption);
        }

        public async Task<LicenseOption> GetByIdAsync(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_options WHERE option_id = @Id;";
            var result = await connection.QueryFirstOrDefaultAsync<LicenseOption>(sql, new { Id = id });
            return result ?? new LicenseOption(); // Return empty object instead of null
        }

        public async Task<IEnumerable<LicenseOption>> GetAllAsync()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_options;";
            return await connection.QueryAsync<LicenseOption>(sql);
        }

        public async Task<IEnumerable<LicenseOption>> GetByLicenseIdAsync(int licenseId)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_options WHERE license_id = @LicenseId;";
            return await connection.QueryAsync<LicenseOption>(sql, new { LicenseId = licenseId });
        }

        public async Task<bool> UpdateAsync(LicenseOption licenseOption)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE license_options
                SET name = @Name, description = @Description, price = @Price, 
                    duration_days = @DurationDays, is_active = @IsActive, last_update_at = @LastUpdateAt
                WHERE option_id = @OptionId;";
            var result = await connection.ExecuteAsync(sql, licenseOption);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM license_options WHERE option_id = @Id;";
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
