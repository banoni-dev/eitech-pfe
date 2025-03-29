
namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/license-orders")]
    public class LicenseOrderController : ControllerBase
    {
        private readonly ILicenseOrderService _licenseOrderService;

        public LicenseOrderController(ILicenseOrderService licenseOrderService)
        {
            _licenseOrderService = licenseOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseOrder licenseOrder)
        {
            licenseOrder.CreatedAt = DateTime.UtcNow;
            licenseOrder.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseOrderService.CreateLicenseOrder(licenseOrder);
            return result > 0 ? Ok("License order created") : BadRequest("Failed to create license order");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var licenseOrder = await _licenseOrderService.GetLicenseOrderById(id);
            return licenseOrder != null ? Ok(licenseOrder) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var licenseOrders = await _licenseOrderService.GetAllLicenseOrders();
            return Ok(licenseOrders);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LicenseOrder licenseOrder)
        {
            licenseOrder.LicenseOrderId = id;
            licenseOrder.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseOrderService.UpdateLicenseOrder(licenseOrder);
            return result > 0 ? Ok("License order updated") : BadRequest("Failed to update license order");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _licenseOrderService.DeleteLicenseOrder(id);
            return result > 0 ? Ok("License order deleted") : BadRequest("Failed to delete license order");
        }
    }
}
