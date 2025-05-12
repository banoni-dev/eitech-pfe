namespace EitechPfe.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseOrderRepository _licenseOrderRepository;

        public LicenseService(ILicenseRepository licenseRepository, ILicenseOrderRepository licenseOrderRepository)
        {
            _licenseRepository = licenseRepository;
            _licenseOrderRepository = licenseOrderRepository;
        }

        public async Task<int> CreateLicense(License license)
        {
            return await _licenseRepository.Add(license);
        }

        public async Task<License?> GetLicenseById(int id)
        {
            return await _licenseRepository.GetById(id);
        }

        public async Task<IEnumerable<License>> GetAllLicenses()
        {
            return await _licenseRepository.GetAll();
        }

        public async Task<int> UpdateLicense(License license)
        {
            return await _licenseRepository.Update(license);
        }

        public async Task<int> DeleteLicense(int id)
        {
            return await _licenseRepository.Delete(id);
        }

        public async Task<bool> CheckLicense(int userId, int licenseId)
        {
            var licenseOrder = await _licenseOrderRepository.GetByUserIdAndLicenseId(userId, licenseId);
            return licenseOrder != null && licenseOrder.Status == LicenseOrderStatus.Active;
        }

        public async Task<bool> CheckLicense(int userId, int productId, string? fingerprint = null)
        {
            var licenseOrder = (await _licenseOrderRepository.GetByUserIdAsync(userId.ToString()))
                .FirstOrDefault(lo => lo.LicenseId == productId && lo.Status == LicenseOrderStatus.Active);

            if (licenseOrder == null)
                return false;

            var license = await _licenseRepository.GetById(licenseOrder.LicenseId);
            if (license == null)
                return false;

            var isLicenseValid = licenseOrder.PurchaseDate.AddDays(license.Duration + license.GracePeriod) >= DateTime.UtcNow;
            if (!isLicenseValid)
                return false;

            if (!string.IsNullOrEmpty(fingerprint))
            {
                var activationCount = licenseOrder.Options?.Count ?? 0;
                if (activationCount >= license.MaxDevices)
                    return false;
            }

            return true;
        }
    }
}
