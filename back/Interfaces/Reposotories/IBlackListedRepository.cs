using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface IBlackListedRepository
    {
        Task<int> CreateBlackListed(BlackListed blackListed);
        Task<BlackListed?> GetBlackListedById(int id);
        Task<IEnumerable<BlackListed>> GetAllBlackListed();
        Task<int> UpdateBlackListed(BlackListed blackListed);
        Task<int> DeleteBlackListed(int id);
    }
}
