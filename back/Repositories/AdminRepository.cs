using Dapper;
using EitechPfe.Entities;
using EitechPfe.Interfaces;
using MySql.Data.MySqlClient;

namespace EitechPfe.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public AdminRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateAdmin(Admin admin)
        {
            using var connection = _dbConfig.GetConnection();
            var query = @"INSERT INTO admins (username, password, api_key, created_at, last_update_at) 
                          VALUES (@Username, @Password, @ApiKey, @CreatedAt, @LastUpdateAt)";
            return await connection.ExecuteAsync(query, admin);
        }

        public async Task<Admin?> GetAdminById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            var query = "SELECT id, username, password, api_key, created_at, last_update_at FROM admins WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Admin>(query, new { Id = id });
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            using var connection = _dbConfig.GetConnection();
            var query = "SELECT id, username, password, api_key, created_at, last_update_at FROM admins";
            return await connection.QueryAsync<Admin>(query);
        }

        public async Task<int> UpdateAdmin(Admin admin)
        {
            using var connection = _dbConfig.GetConnection();
            var query = @"UPDATE admins SET username = @Username, password = @Password, api_key = @ApiKey,
                          last_update_at = @LastUpdateAt WHERE id = @Id";
            return await connection.ExecuteAsync(query, admin);
        }

        public async Task<int> DeleteAdmin(int id)
        {
            using var connection = _dbConfig.GetConnection();
            var query = "DELETE FROM admins WHERE id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
