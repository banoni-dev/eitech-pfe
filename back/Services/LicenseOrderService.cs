
namespace EitechPfe.Services
{
    public class LicenseOrderService : ILicenseOrderService
    {
        private readonly ILicenseOrderRepository _licenseOrderRepository;

        public LicenseOrderService(ILicenseOrderRepository licenseOrderRepository)
        {
            _licenseOrderRepository = licenseOrderRepository;
        }

        public async Task<int> CreateLicenseOrder(LicenseOrder licenseOrder)
        {
            return await _licenseOrderRepository.CreateAsync(licenseOrder);
        }

        public async Task<int> DeleteLicenseOrder(int id)
        {
            var result = await _licenseOrderRepository.DeleteAsync(id);
            return result ? 1 : 0;
        }

        public async Task<IEnumerable<LicenseOrder>> GetAllLicenseOrders()
        {
            return await _licenseOrderRepository.GetAllAsync();
        }

        public async Task<LicenseOrder?> GetLicenseOrderById(int id)
        {
            return await _licenseOrderRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<int> UpdateLicenseOrder(LicenseOrder licenseOrder)
        {
            var result = await _licenseOrderRepository.UpdateAsync(licenseOrder);
            return result ? 1 : 0;
        }
    }
}
