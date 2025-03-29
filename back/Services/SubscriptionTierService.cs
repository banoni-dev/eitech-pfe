namespace EitechPfe.Services
{
    public class SubscriptionTierService : ISubscriptionTierService
    {
        private readonly ISubscriptionTierRepository _subscriptionTierRepository;

        public SubscriptionTierService(ISubscriptionTierRepository subscriptionTierRepository)
        {
            _subscriptionTierRepository = subscriptionTierRepository;
        }

        public async Task<int> CreateSubscriptionTier(SubscriptionTier subscriptionTier)
        {
            return await _subscriptionTierRepository.CreateSubscriptionTier(subscriptionTier);
        }

        public async Task<SubscriptionTier?> GetSubscriptionTierById(int id)
        {
            return await _subscriptionTierRepository.GetSubscriptionTierById(id);
        }

        public async Task<IEnumerable<SubscriptionTier>> GetAllSubscriptionTiers()
        {
            return await _subscriptionTierRepository.GetAllSubscriptionTiers();
        }

        public async Task<int> UpdateSubscriptionTier(SubscriptionTier subscriptionTier)
        {
            return await _subscriptionTierRepository.UpdateSubscriptionTier(subscriptionTier);
        }

        public async Task<int> DeleteSubscriptionTier(int id)
        {
            return await _subscriptionTierRepository.DeleteSubscriptionTier(id);
        }
    }
}
