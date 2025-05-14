namespace EitechPfe.Interfaces
{
    public interface IBlackListedService
    {
        Task<int> CreateBlackListed(BlackListed blackListed);
        Task<BlackListed?> GetBlackListedById(int id);
        Task<IEnumerable<BlackListed>> GetAllBlackListed();
        Task<int> UpdateBlackListed(BlackListed blackListed);
        Task<int> DeleteBlackListed(int id);
        // Task<bool> IsIpBlacklisted(string ip);
        // Task<bool> IsUserBlacklisted(int userId);
        // Task<bool> IsDeviceBlacklisted(string deviceFingerprint);
    }
}
