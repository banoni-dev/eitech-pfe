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
                INSERT INTO license_activations (license_order_id, device_fingerprint, activation_date, created_at, last_update_at, is_archived)
                VALUES (@LicenseOrderId, @DeviceFingerprint, @ActivationDate, @CreatedAt, @LastUpdateAt, @IsArchived);
                SELECT LAST_INSERT_ID();";
            return await connection.ExecuteScalarAsync<int>(sql, licenseActivation);
        }

        public async Task<LicenseActivation?> GetById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_activations WHERE activation_id = @Id AND is_archived = FALSE;";
            return await connection.QueryFirstOrDefaultAsync<LicenseActivation>(sql, new { Id = id });
        }

        public async Task<IEnumerable<LicenseActivation>> GetAll()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_activations WHERE is_archived = FALSE;";
            return await connection.QueryAsync<LicenseActivation>(sql);
        }

        public async Task<int> Update(LicenseActivation licenseActivation)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE license_activations
                SET license_order_id = @LicenseOrderId, device_fingerprint = @DeviceFingerprint, 
                    activation_date = @ActivationDate, last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE activation_id = @ActivationId;";
            return await connection.ExecuteAsync(sql, licenseActivation);
        }

        public async Task<int> Delete(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "UPDATE license_activations SET is_archived = TRUE WHERE activation_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}