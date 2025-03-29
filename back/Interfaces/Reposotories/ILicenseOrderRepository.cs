
namespace EitechPfe.Interfaces
{
    public interface ILicenseOrderRepository
    {
        Task<IEnumerable<LicenseOrder>> GetAllAsync();
        Task<LicenseOrder> GetByIdAsync(int id);
        Task<LicenseOrder?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<LicenseOrder>> GetByUserIdAsync(string userId);
        Task<int> CreateAsync(LicenseOrder order);
        Task<bool> UpdateAsync(LicenseOrder order);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddOptionToOrderAsync(LicenseOrderOption orderOption);
    }
}
