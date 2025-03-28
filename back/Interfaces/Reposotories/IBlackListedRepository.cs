using EitechPfe.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Interfaces
{
    public interface IBlackListedRepository
    {
        Task<int> Add(BlackListed blackListed);
        Task<BlackListed?> GetById(int id);
        Task<IEnumerable<BlackListed>> GetAll();
        Task<int> Update(BlackListed blackListed);
        Task<int> Delete(int id);
    }
}
