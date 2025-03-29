using Dapper;
using System.Data;
using EitechPfe.Entities;
using EitechPfe.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EitechPfe.Repositories
{
    public class LicenseOrderRepository : ILicenseOrderRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public LicenseOrderRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateAsync(LicenseOrder licenseOrder)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO license_orders (user_id, license_id, private_key, purchase_date, status, created_at, last_update_at, is_archived)
                VALUES (@UserId, @LicenseId, @PrivateKey, @PurchaseDate, @Status, @CreatedAt, @LastUpdateAt, @IsArchived);
                SELECT LAST_INSERT_ID();";
            return await connection.ExecuteScalarAsync<int>(sql, licenseOrder);
        }

        public async Task<LicenseOrder> GetByIdAsync(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_orders WHERE license_order_id = @Id;";
            var result = await connection.QueryFirstOrDefaultAsync<LicenseOrder>(sql, new { Id = id });
            return result ?? new LicenseOrder(); // Return an empty object instead of null
        }

        public async Task<LicenseOrder?> GetByIdWithDetailsAsync(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT * FROM license_orders WHERE license_order_id = @Id;";
            
            var licenseOrder = await connection.QueryFirstOrDefaultAsync<LicenseOrder>(sql, new { Id = id });
            
            if (licenseOrder != null)
            {
                // Get order options if they exist
                string optionsSql = @"
                    SELECT * FROM license_order_options WHERE license_order_id = @OrderId;";
                
                var options = await connection.QueryAsync<LicenseOrderOption>(optionsSql, new { OrderId = id });
                licenseOrder.Options = options.ToList();
            }
            
            return licenseOrder;
        }

        public async Task<IEnumerable<LicenseOrder>> GetAllAsync()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_orders;";
            return await connection.QueryAsync<LicenseOrder>(sql);
        }

        public async Task<IEnumerable<LicenseOrder>> GetByUserIdAsync(string userId)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM license_orders WHERE user_id = @UserId;";
            return await connection.QueryAsync<LicenseOrder>(sql, new { UserId = userId });
        }

        public async Task<bool> UpdateAsync(LicenseOrder licenseOrder)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE license_orders
                SET user_id = @UserId, license_id = @LicenseId, private_key = @PrivateKey,
                    purchase_date = @PurchaseDate, status = @Status, 
                    last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE license_order_id = @LicenseOrderId;";
            var result = await connection.ExecuteAsync(sql, licenseOrder);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbConfig.GetConnection();
            
            // First delete related records in license_order_options
            await connection.ExecuteAsync("DELETE FROM license_order_options WHERE license_order_id = @Id;", new { Id = id });
            
            // Then delete the order
            string sql = "DELETE FROM license_orders WHERE license_order_id = @Id;";
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<bool> AddOptionToOrderAsync(LicenseOrderOption orderOption)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO license_order_options (license_order_id, option_id, quantity, unit_price)
                VALUES (@LicenseOrderId, @OptionId, @Quantity, @UnitPrice);";
            var result = await connection.ExecuteAsync(sql, orderOption);
            return result > 0;
        }
    }
}
