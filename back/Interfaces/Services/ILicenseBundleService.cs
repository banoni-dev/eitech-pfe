using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface ILicenseBundleService
    {
        Task<int> CreateLicenseBundle(LicenseBundle licenseBundle);
        Task<LicenseBundle?> GetLicenseBundleById(int id);
        Task<IEnumerable<LicenseBundle>> GetAllLicenseBundles();
        Task<int> UpdateLicenseBundle(LicenseBundle licenseBundle);
        Task<int> DeleteLicenseBundle(int id);
    }
}
