
namespace EitechPfe.Repositories
{
    public class LicenseActivationRepository : ILicenseActivationRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public LicenseActivationRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> Add(LicenseActivation licenseActivation)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO license_activations (license_id, device_identifier, activation_date, is_active, created_at, last_update_at)
                VALUES (@LicenseId, @DeviceIdentifier, @ActivationDate, @IsActive, @CreatedAt, @LastUpdateAt);
                SELECT LAST_INSERT_ID();";
            return await connection.ExecuteScalarAsync<int>(sql, licenseActivation);
        }

        public async Task<LicenseActivation?> GetById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_activations WHERE activation_id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<LicenseActivation>(sql, new { Id = id });
        }

        public async Task<IEnumerable<LicenseActivation>> GetAll()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_activations;";
            return await connection.QueryAsync<LicenseActivation>(sql);
        }

        public async Task<int> Update(LicenseActivation licenseActivation)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE license_activations
                SET license_id = @LicenseId, device_identifier = @DeviceIdentifier, 
                    activation_date = @ActivationDate, deactivation_date = @DeactivationDate,
                    is_active = @IsActive, last_update_at = @LastUpdateAt
                WHERE activation_id = @ActivationId;";
            return await connection.ExecuteAsync(sql, licenseActivation);
        }

        public async Task<int> Delete(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM license_activations WHERE activation_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
