public class SubscriptionOrderService : ISubscriptionOrderService
{
    private readonly ISubscriptionOrderRepository _subscriptionOrderRepository;

    public SubscriptionOrderService(ISubscriptionOrderRepository subscriptionOrderRepository)
    {
        _subscriptionOrderRepository = subscriptionOrderRepository;
    }

    public Task<int> CreateSubscriptionOrder(SubscriptionOrder subscriptionOrder) => _subscriptionOrderRepository.CreateSubscriptionOrder(subscriptionOrder);
    public Task<SubscriptionOrder?> GetSubscriptionOrderById(int id) => _subscriptionOrderRepository.GetSubscriptionOrderById(id);
    public Task<IEnumerable<SubscriptionOrder>> GetAllSubscriptionOrders() => _subscriptionOrderRepository.GetAllSubscriptionOrders();
    public Task<int> UpdateSubscriptionOrder(SubscriptionOrder subscriptionOrder) => _subscriptionOrderRepository.UpdateSubscriptionOrder(subscriptionOrder);
    public Task<int> DeleteSubscriptionOrder(int id) => _subscriptionOrderRepository.DeleteSubscriptionOrder(id);
}
