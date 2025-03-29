using EitechPfe.Entities;
using EitechPfe.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Services
{
    public class LicenseBundleService : ILicenseBundleService
    {
        private readonly ILicenseBundleRepository _licenseBundleRepository;

        public LicenseBundleService(ILicenseBundleRepository licenseBundleRepository)
        {
            _licenseBundleRepository = licenseBundleRepository;
        }

        public async Task<int> CreateLicenseBundle(LicenseBundle licenseBundle)
        {
            return await _licenseBundleRepository.Add(licenseBundle);
        }

        public async Task<LicenseBundle?> GetLicenseBundleById(int id)
        {
            return await _licenseBundleRepository.GetById(id);
        }

        public async Task<IEnumerable<LicenseBundle>> GetAllLicenseBundles()
        {
            return await _licenseBundleRepository.GetAll();
        }

        public async Task<int> UpdateLicenseBundle(LicenseBundle licenseBundle)
        {
            return await _licenseBundleRepository.Update(licenseBundle);
        }

        public async Task<int> DeleteLicenseBundle(int id)
        {
            return await _licenseBundleRepository.Delete(id);
        }
    }
}
