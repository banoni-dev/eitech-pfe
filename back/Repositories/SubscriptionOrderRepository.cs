namespace EitechPfe.Repositories
{
    public class SubscriptionOrderRepository : ISubscriptionOrderRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public SubscriptionOrderRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateSubscriptionOrder(SubscriptionOrder subscriptionOrder)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO subscription_orders (subscription_tier_id, user_id, purchase_date, start_date, end_date, status, created_at, last_update_at, is_archived)
                VALUES (@SubscriptionTierId, @UserId, @PurchaseDate, @StartDate, @EndDate, @Status, @CreatedAt, @LastUpdateAt, @IsArchived);
                SELECT LAST_INSERT_ID();"; // Return the generated Id

            // Map the Status enum to a string
            var dbSubscriptionOrder = new
            {
                subscriptionOrder.SubscriptionTierId,
                subscriptionOrder.UserId,
                subscriptionOrder.PurchaseDate,
                subscriptionOrder.StartDate,
                subscriptionOrder.EndDate,
                Status = subscriptionOrder.Status.ToString(),
                subscriptionOrder.CreatedAt,
                subscriptionOrder.LastUpdateAt,
                subscriptionOrder.IsArchived
            };

            return await connection.ExecuteAsync(sql, dbSubscriptionOrder);
        }

        public async Task<SubscriptionOrder?> GetSubscriptionOrderById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM subscription_orders WHERE id = @Id;"; // Use Id for lookup
            return await connection.QueryFirstOrDefaultAsync<SubscriptionOrder>(sql, new { Id = id });
        }

        public async Task<IEnumerable<SubscriptionOrder>> GetAllSubscriptionOrders()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "SELECT * FROM subscription_orders;";
            return await connection.QueryAsync<SubscriptionOrder>(sql);
        }

        public async Task<int> UpdateSubscriptionOrder(SubscriptionOrder subscriptionOrder)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE subscription_orders
                SET subscription_tier_id = @SubscriptionTierId, user_id = @UserId, purchase_date = @PurchaseDate, 
                    start_date = @StartDate, end_date = @EndDate, status = @Status, last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE id = @Id;"; // Use Id for update
            return await connection.ExecuteAsync(sql, subscriptionOrder);
        }

        public async Task<int> DeleteSubscriptionOrder(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM subscription_orders WHERE id = @Id;"; // Use Id for deletion
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
