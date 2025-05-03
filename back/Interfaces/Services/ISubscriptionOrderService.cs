namespace EitechPfe.Interfaces
{
    public interface ISubscriptionOrderService
    {
        Task<int> CreateSubscriptionOrder(SubscriptionOrder subscriptionOrder);
        Task<SubscriptionOrder?> GetSubscriptionOrderById(int id);
        Task<IEnumerable<SubscriptionOrder>> GetAllSubscriptionOrders();
        Task<int> UpdateSubscriptionOrder(SubscriptionOrder subscriptionOrder);
        Task<int> DeleteSubscriptionOrder(int id);
        Task<bool> CheckSubscription(int userId, int productId);
    }
}
