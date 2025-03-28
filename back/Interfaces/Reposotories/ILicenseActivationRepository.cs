using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface ILicenseActivationRepository
    {
        Task<int> Add(LicenseActivation licenseActivation);
        Task<LicenseActivation?> GetById(int id);
        Task<IEnumerable<LicenseActivation>> GetAll();
        Task<int> Update(LicenseActivation licenseActivation);
        Task<int> Delete(int id);
    }
}
