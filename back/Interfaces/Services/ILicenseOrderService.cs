using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface ILicenseOrderService
    {
        Task<int> CreateLicenseOrder(LicenseOrder licenseOrder);
        Task<LicenseOrder?> GetLicenseOrderById(int id);
        Task<IEnumerable<LicenseOrder>> GetAllLicenseOrders();
        Task<int> UpdateLicenseOrder(LicenseOrder licenseOrder);
        Task<int> DeleteLicenseOrder(int id);
    }
}
