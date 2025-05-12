namespace EitechPfe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public UserRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateUser(User user)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO users (first_name, last_name, email, phone, created_at, last_update_at, is_archived)
                VALUES (@FirstName, @LastName, @Email, @Phone, @CreatedAt, @LastUpdateAt, @IsArchived);";
            return await connection.ExecuteAsync(sql, user);
        }

        public async Task<User?> GetUserById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    user_id AS UserId,
                    first_name AS FirstName,
                    last_name AS LastName,
                    email,
                    phone,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM users
                WHERE user_id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    user_id AS UserId,
                    first_name AS FirstName,
                    last_name AS LastName,
                    email,
                    phone,
                    created_at AS CreatedAt,
                    last_update_at AS LastUpdateAt,
                    is_archived AS IsArchived
                FROM users;";
                
            return await connection.QueryAsync<User>(sql);
        }


        public async Task<int> UpdateUser(User user)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE users
                SET first_name = @FirstName, last_name = @LastName, email = @Email, phone = @Phone, 
                    last_update_at = @LastUpdateAt, is_archived = @IsArchived
                WHERE user_id = @UserId;";
            return await connection.ExecuteAsync(sql, user);
        }

        public async Task<int> DeleteUser(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM users WHERE user_id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
