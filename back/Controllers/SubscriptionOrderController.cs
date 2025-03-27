using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/subscription-orders")]
public class SubscriptionOrderController : ControllerBase
{
    private readonly ISubscriptionOrderService _subscriptionOrderService;

    public SubscriptionOrderController(ISubscriptionOrderService subscriptionOrderService)
    {
        _subscriptionOrderService = subscriptionOrderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubscriptionOrder subscriptionOrder)
    {
        subscriptionOrder.CreatedAt = DateTime.UtcNow;
        subscriptionOrder.LastUpdateAt = DateTime.UtcNow;

        var result = await _subscriptionOrderService.CreateSubscriptionOrder(subscriptionOrder);
        return result > 0 ? Ok("Subscription order created") : BadRequest("Failed to create subscription order");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var subscriptionOrder = await _subscriptionOrderService.GetSubscriptionOrderById(id);
        return subscriptionOrder != null ? Ok(subscriptionOrder) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscriptionOrders = await _subscriptionOrderService.GetAllSubscriptionOrders();
        return Ok(subscriptionOrders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SubscriptionOrder subscriptionOrder)
    {
        subscriptionOrder.SubscriptionTierId = id;
        subscriptionOrder.LastUpdateAt = DateTime.UtcNow;

        var result = await _subscriptionOrderService.UpdateSubscriptionOrder(subscriptionOrder);
        return result > 0 ? Ok("Subscription order updated") : BadRequest("Failed to update subscription order");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _subscriptionOrderService.DeleteSubscriptionOrder(id);
        return result > 0 ? Ok("Subscription order deleted") : BadRequest("Failed to delete subscription order");
    }
}
