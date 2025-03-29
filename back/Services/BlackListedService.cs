namespace EitechPfe.Services
{
    public class BlackListedService : IBlackListedService
    {
        private readonly IBlackListedRepository _blackListedRepository;

        public BlackListedService(IBlackListedRepository blackListedRepository)
        {
            _blackListedRepository = blackListedRepository;
        }

        public async Task<int> CreateBlackListed(BlackListed blackListed)
        {
            return await _blackListedRepository.CreateBlackListed(blackListed);
        }

        public async Task<BlackListed?> GetBlackListedById(int id)
        {
            return await _blackListedRepository.GetBlackListedById(id);
        }

        public async Task<IEnumerable<BlackListed>> GetAllBlackListed()
        {
            return await _blackListedRepository.GetAllBlackListed();
        }

        public async Task<int> UpdateBlackListed(BlackListed blackListed)
        {
            return await _blackListedRepository.UpdateBlackListed(blackListed);
        }

        public async Task<int> DeleteBlackListed(int id)
        {
            return await _blackListedRepository.DeleteBlackListed(id);
        }
    }
}
