using EitechPfe.Models;

namespace EitechPfe.Repositories
{
    public interface ILicenseRepository
    {
        Task<int> Add(License license);
        Task<License?> GetById(int id);
        Task<IEnumerable<License>> GetAll();
        Task<int> Update(License license);
        Task<int> Delete(int id);
    }
}
