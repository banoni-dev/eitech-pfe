using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface ILicenseService
    {
        Task<int> CreateLicense(License license);
        Task<License?> GetLicenseById(int id);
        Task<IEnumerable<License>> GetAllLicenses();
        Task<int> UpdateLicense(License license);
        Task<int> DeleteLicense(int id);
    }
}
