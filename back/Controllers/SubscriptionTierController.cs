using Microsoft.AspNetCore.Mvc;

namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/subscription-tiers")]
    public class SubscriptionTierController : ControllerBase
    {
        private readonly ISubscriptionTierService _subscriptionTierService;

        public SubscriptionTierController(ISubscriptionTierService subscriptionTierService)
        {
            _subscriptionTierService = subscriptionTierService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionTier subscriptionTier)
        {
            subscriptionTier.CreatedAt = DateTime.UtcNow;
            subscriptionTier.LastUpdateAt = DateTime.UtcNow;

            var result = await _subscriptionTierService.CreateSubscriptionTier(subscriptionTier);
            return result > 0 ? Ok("Subscription tier created") : BadRequest("Failed to create subscription tier");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscriptionTier = await _subscriptionTierService.GetSubscriptionTierById(id);
            return subscriptionTier != null ? Ok(subscriptionTier) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscriptionTiers = await _subscriptionTierService.GetAllSubscriptionTiers();
            return Ok(subscriptionTiers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubscriptionTier subscriptionTier)
        {
            subscriptionTier.TierId = id;
            subscriptionTier.LastUpdateAt = DateTime.UtcNow;

            var result = await _subscriptionTierService.UpdateSubscriptionTier(subscriptionTier);
            return result > 0 ? Ok("Subscription tier updated") : BadRequest("Failed to update subscription tier");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _subscriptionTierService.DeleteSubscriptionTier(id);
            return result > 0 ? Ok("Subscription tier deleted") : BadRequest("Failed to delete subscription tier");
        }
    }
}
