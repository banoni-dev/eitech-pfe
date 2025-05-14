
namespace EitechPfe.Interfaces
{
    public interface ILicenseOptionRepository
    {
        Task<IEnumerable<LicenseOption>> GetAllAsync();
        Task<LicenseOption> GetByIdAsync(int id);
        Task<IEnumerable<LicenseOption>> GetByLicenseIdAsync(int licenseId);
        Task<int> CreateAsync(LicenseOption option);
        Task<bool> UpdateAsync(LicenseOption option);
        Task<bool> DeleteAsync(int id);
    }
}
