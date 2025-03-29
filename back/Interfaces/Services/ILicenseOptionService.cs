
namespace EitechPfe.Interfaces
{
    public interface ILicenseOptionService
    {
        Task<int> CreateLicenseOption(LicenseOption licenseOption);
        Task<LicenseOption?> GetLicenseOptionById(int id);
        Task<IEnumerable<LicenseOption>> GetAllLicenseOptions();
        Task<int> UpdateLicenseOption(LicenseOption licenseOption);
        Task<int> DeleteLicenseOption(int id);
    }
}
