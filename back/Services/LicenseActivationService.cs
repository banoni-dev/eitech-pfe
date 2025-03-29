public class LicenseActivationService : ILicenseActivationService
    {
        private readonly ILicenseActivationRepository _repository;

        public LicenseActivationService(ILicenseActivationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateLicenseActivation(LicenseActivation licenseActivation)
        {
            return await _repository.Add(licenseActivation);
        }

        public async Task<LicenseActivation?> GetLicenseActivationById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<LicenseActivation>> GetAllLicenseActivations()
        {
            return await _repository.GetAll();
        }

        public async Task<int> UpdateLicenseActivation(LicenseActivation licenseActivation)
        {
            return await _repository.Update(licenseActivation);
        }

        public async Task<int> DeleteLicenseActivation(int id)
        {
            return await _repository.Delete(id);
        }
    }

