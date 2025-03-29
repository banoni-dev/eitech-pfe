namespace EitechPfe.Services
{
    public class SubscriptionOrderService : ISubscriptionOrderService
    {
        private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

        public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
        {
            _subscriptionOrderRepository = subscriptionOrderRepository;
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
            return await _subscriptionOrderRepository.UpdateSubscriptionOrder(subscriptionOrder);
        }

        public async Task<int> DeleteSubscriptionOrder(int id)
        {
            return await _subscriptionOrderRepository.DeleteSubscriptionOrder(id);
        }
    }
}
