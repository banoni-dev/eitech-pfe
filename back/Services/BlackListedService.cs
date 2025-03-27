public class BlackListedService : IBlackListedService
{
    private readonly IBlackListedRepository _blackListedRepository;

    public BlackListedService(IBlackListedRepository blackListedRepository)
    {
        _blackListedRepository = blackListedRepository;
    }

    public Task<int> CreateBlackListed(BlackListed blackListed) => _blackListedRepository.CreateBlackListed(blackListed);
    public Task<BlackListed?> GetBlackListedById(int id) => _blackListedRepository.GetBlackListedById(id);
    public Task<IEnumerable<BlackListed>> GetAllBlackListed() => _blackListedRepository.GetAllBlackListed();
    public Task<int> UpdateBlackListed(BlackListed blackListed) => _blackListedRepository.UpdateBlackListed(blackListed);
    public Task<int> DeleteBlackListed(int id) => _blackListedRepository.DeleteBlackListed(id);
}
