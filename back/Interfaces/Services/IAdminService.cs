using EitechPfe.DTOs.Requests;
using EitechPfe.DTOs.Responses;
using EitechPfe.Entities;

namespace EitechPfe.Interfaces
{
    public interface IAdminService
    {
        Task<int> CreateAdmin(AdminRequest request);
        Task<AdminResponse?> GetAdminById(int id);
        Task<IEnumerable<AdminResponse>> GetAllAdmins();
        Task<int> UpdateAdmin(int id, AdminRequest request);
        Task<int> DeleteAdmin(int id);
        Task<string> GenerateApiKey(int id);
    }
}
