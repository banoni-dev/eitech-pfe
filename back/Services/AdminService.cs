using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;
using EitechPfe.Entities;
using EitechPfe.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EitechPfe.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<int> CreateAdmin(AdminRequest request)
        {
            var admin = new Admin
            {
                Username = request.Username,
                Password = HashPassword(request.Password)
            };
            return await _adminRepository.CreateAdmin(admin);
        }

        public async Task<AdminResponse?> GetAdminById(int id)
        {
            var admin = await _adminRepository.GetAdminById(id);
            return admin == null ? null : MapToResponse(admin);
        }

        public async Task<IEnumerable<AdminResponse>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAdmins();
            return admins.Select(MapToResponse);
        }

        public async Task<int> UpdateAdmin(int id, AdminRequest request)
        {
            var admin = new Admin
            {
                Id = id,
                Username = request.Username,
                Password = HashPassword(request.Password),
                LastUpdateAt = DateTime.UtcNow
            };
            return await _adminRepository.UpdateAdmin(admin);
        }

        public async Task<int> DeleteAdmin(int id)
        {
            return await _adminRepository.DeleteAdmin(id);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static AdminResponse MapToResponse(Admin admin)
        {
            return new AdminResponse
            {
                Id = admin.Id,
                Username = admin.Username,
                CreatedAt = admin.CreatedAt,
                LastUpdateAt = admin.LastUpdateAt
            };
        }
    }
}
