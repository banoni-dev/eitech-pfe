using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface ILicenseBundleRepository
    {
        Task<int> Add(LicenseBundle licenseBundle);
        Task<LicenseBundle?> GetById(int id);
        Task<IEnumerable<LicenseBundle>> GetAll();
        Task<int> Update(LicenseBundle licenseBundle);
        Task<int> Delete(int id);
    }
}
