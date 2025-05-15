using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;
using EitechPfe.Entities;
using EitechPfe.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;
using BCrypt.Net;

namespace EitechPfe.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDbConnection _connection;

        public AdminService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateAdmin(AdminRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var apiKey = GenerateSecureApiKey();
            
            var sql = @"
                INSERT INTO admins (username, password, api_key, created_at, last_update_at)
                VALUES (@Username, @Password, @ApiKey, @CreatedAt, @LastUpdateAt);
                SELECT LAST_INSERT_ID();";
                
            var parameters = new
            {
                request.Username,
                Password = hashedPassword,
                ApiKey = apiKey,
                CreatedAt = DateTime.UtcNow,
                LastUpdateAt = DateTime.UtcNow
            };
            
            return await _connection.ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<AdminResponse?> GetAdminById(int id)
        {
            var sql = "SELECT id, username, api_key AS ApiKey, created_at, last_update_at FROM admins WHERE id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<AdminResponse>(sql, new { Id = id });
        }

        public async Task<IEnumerable<AdminResponse>> GetAllAdmins()
        {
            var sql = "SELECT id, username, api_key AS ApiKey, created_at, last_update_at FROM admins";
            return await _connection.QueryAsync<AdminResponse>(sql);
        }

        public async Task<int> UpdateAdmin(int id, AdminRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            
            var sql = @"
                UPDATE admins 
                SET username = @Username, 
                    password = @Password, 
                    last_update_at = @LastUpdateAt 
                WHERE id = @Id";
                
            var parameters = new
            {
                Id = id,
                request.Username,
                Password = hashedPassword,
                LastUpdateAt = DateTime.UtcNow
            };
            
            return await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<int> DeleteAdmin(int id)
        {
            var sql = "DELETE FROM admins WHERE id = @Id";
            return await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<string> GenerateApiKey(int id)
        {
            var apiKey = GenerateSecureApiKey();
            
            var sql = @"
                UPDATE admins 
                SET api_key = @ApiKey, 
                    last_update_at = @LastUpdateAt 
                WHERE id = @Id";
                
            var parameters = new
            {
                Id = id,
                ApiKey = apiKey,
                LastUpdateAt = DateTime.UtcNow
            };
            
            var result = await _connection.ExecuteAsync(sql, parameters);
            return result > 0 ? apiKey : string.Empty;
        }

        private string GenerateSecureApiKey()
        {
            // Generate a 32-byte (256-bit) random key
            using var rng = RandomNumberGenerator.Create();
            var keyBytes = new byte[32];
            rng.GetBytes(keyBytes);
            
            // Convert to a base64 string and remove characters that might cause issues in URLs
            return Convert.ToBase64String(keyBytes)
                .Replace("+", "")
                .Replace("/", "")
                .Replace("=", "")
                .Substring(0, 40); // Truncate to a reasonable length
        }
    }
}
