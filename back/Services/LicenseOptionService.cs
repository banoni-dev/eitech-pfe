
namespace EitechPfe.Services
{
    public class LicenseOptionService : ILicenseOptionService
    {
        private readonly ILicenseOptionRepository _licenseOptionRepository;

        public LicenseOptionService(ILicenseOptionRepository licenseOptionRepository)
        {
            _licenseOptionRepository = licenseOptionRepository;
        }

        public async Task<int> CreateLicenseOption(LicenseOption licenseOption)
        {
            return await _licenseOptionRepository.CreateAsync(licenseOption);
        }

        public async Task<int> DeleteLicenseOption(int id)
        {
            var result = await _licenseOptionRepository.DeleteAsync(id);
            return result ? 1 : 0;
        }

        public async Task<IEnumerable<LicenseOption>> GetAllLicenseOptions()
        {
            return await _licenseOptionRepository.GetAllAsync();
        }

        public async Task<LicenseOption?> GetLicenseOptionById(int id)
        {
            return await _licenseOptionRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateLicenseOption(LicenseOption licenseOption)
        {
            var result = await _licenseOptionRepository.UpdateAsync(licenseOption);
            return result ? 1 : 0;
        }
    }
}
