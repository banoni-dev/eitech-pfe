namespace EitechPfe.Services
{
    public class SubscriptionOrderService : ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;
        private readonly ISubscriptionTierRepository _subscriptionTierRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository, ISubscriptionTierRepository subscriptionTierRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
            _subscriptionTierRepository = subscriptionTierRepository;
        }

        public async Task<int> CreateSubscriptionOrder(SubscriptionOrder subscriptionOrder)
        {
            return await _subscriptionOrderRepository.CreateSubscriptionOrder(subscriptionOrder);
        }

        public async Task<SubscriptionOrder?> GetSubscriptionOrderById(int id)
        {
            return await _subscriptionOrderRepository.GetSubscriptionOrderById(id);
        }

        public async Task<IEnumerable<SubscriptionOrder>> GetAllSubscriptionOrders()
        {
            return await _subscriptionOrderRepository.GetAllSubscriptionOrders();
        }

        public async Task<int> UpdateSubscriptionOrder(SubscriptionOrder subscriptionOrder)
        {
            // Ensure the Id is used for updating the subscription order
            return await _subscriptionOrderRepository.UpdateSubscriptionOrder(subscriptionOrder);
        }

        public async Task<int> DeleteSubscriptionOrder(int id)
        {
            return await _subscriptionOrderRepository.DeleteSubscriptionOrder(id);
        }

        public async Task<bool> CheckSubscription(int userId, int productId)
        {
            var subscriptionOrder = (await _subscriptionOrderRepository.GetAllSubscriptionOrders())
                .FirstOrDefault(so => so.UserId == userId && so.Status == SubscriptionStatus.Active);

            if (subscriptionOrder == null)
                return false;

            var subscriptionTier = await _subscriptionTierRepository.GetSubscriptionTierById(subscriptionOrder.SubscriptionTierId);
            if (subscriptionTier == null || subscriptionTier.ProductId != productId)
                return false;

            var isSubscriptionValid = subscriptionOrder.EndDate.AddDays(subscriptionTier.GracePeriod) >= DateTime.UtcNow;
            return isSubscriptionValid;
        }
    }
}
