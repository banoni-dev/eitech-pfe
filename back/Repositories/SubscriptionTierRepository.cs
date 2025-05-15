namespace EitechPfe.Repositories
{
    public class SubscriptionTierRepository : ISubscriptionTierRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public SubscriptionTierRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateSubscriptionTier(SubscriptionTier subscriptionTier)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, created_at, last_update_at, is_archived)
                VALUES (@ProductId, @TierName, @Duration, @GracePeriod, @Price, @CreatedAt, @LastUpdateAt, @IsArchived);";
            return await connection.ExecuteAsync(sql, subscriptionTier);
        }

        public async Task<SubscriptionTier?> GetSubscriptionTierById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    tier_id AS TierId,
                    product_id AS ProductId,
                    tier_name AS TierName,
                    duration AS Duration,
                    grace_period AS GracePeriod,
                    price AS Price,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM subscription_tiers
                WHERE tier_id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<SubscriptionTier>(sql, new { Id = id });
        }

        public async Task<IEnumerable<SubscriptionTier>> GetAllSubscriptionTiers()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    tier_id AS TierId,
                    product_id AS ProductId,
                    tier_name AS TierName,
                    duration AS Duration,
                    grace_period AS GracePeriod,
                    price AS Price,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM subscription_tiers;";
            return await connection.QueryAsync<SubscriptionTier>(sql);
        }
        // LOAD THE EXISTING RECORDS FROM THE DATABASE THEN MERGE
        public async Task<int> UpdateSubscriptionTier(SubscriptionTier subscriptionTier)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE subscription_tiers
                SET product_id = @ProductId, tier_name = @TierName, duration = @Duration, 
                    grace_period = @GracePeriod, price = @Price, last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE tier_id = @TierId;";
            return await connection.ExecuteAsync(sql, subscriptionTier);
        }

        public async Task<int> DeleteSubscriptionTier(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM subscription_tiers WHERE tier_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
