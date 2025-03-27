public interface ISubscriptionOrderRepository
{
    Task<int> CreateSubscriptionOrder(SubscriptionOrder subscriptionOrder);
    Task<SubscriptionOrder?> GetSubscriptionOrderById(int id);
    Task<IEnumerable<SubscriptionOrder>> GetAllSubscriptionOrders();
    Task<int> UpdateSubscriptionOrder(SubscriptionOrder subscriptionOrder);
    Task<int> DeleteSubscriptionOrder(int id);
}
