using EitechPfe.Entities;
using EitechPfe.Interfaces;

namespace EitechPfe.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _repository;

        public LicenseService(ILicenseRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateLicense(License license)
        {
            return await _repository.Add(license);
        }

        public async Task<License?> GetLicenseById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<License>> GetAllLicenses()
        {
            return await _repository.GetAll();
        }

        public async Task<int> UpdateLicense(License license)
        {
            return await _repository.Update(license);
        }

        public async Task<int> DeleteLicense(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
