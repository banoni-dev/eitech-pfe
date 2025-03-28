using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
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
