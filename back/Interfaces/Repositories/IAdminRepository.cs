using EitechPfe.Entities;

namespace EitechPfe.Interfaces
{
    public interface IAdminRepository
    {
        Task<int> CreateAdmin(Admin admin);
        Task<Admin?> GetAdminById(int id);
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task<int> UpdateAdmin(Admin admin);
        Task<int> DeleteAdmin(int id);
    }
}
