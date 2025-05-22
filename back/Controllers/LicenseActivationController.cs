namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/license-activations")]
    public class LicenseActivationController : ControllerBase
    {
        private readonly ILicenseActivationService _licenseActivationService;

        public LicenseActivationController(ILicenseActivationService licenseActivationService)
        {
            _licenseActivationService = licenseActivationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseActivation licenseActivation)
        {
            licenseActivation.CreatedAt = DateTime.UtcNow;
            licenseActivation.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseActivationService.CreateLicenseActivation(licenseActivation);
            return result > 0 ? Ok("License activation created") : BadRequest("Failed to create license activation");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var licenseActivation = await _licenseActivationService.GetLicenseActivationById(id);
            return licenseActivation != null ? Ok(licenseActivation) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var licenseActivations = await _licenseActivationService.GetAllLicenseActivations();
            return Ok(licenseActivations);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LicenseActivation licenseActivation)
        {
            licenseActivation.ActivationId = id;
            licenseActivation.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseActivationService.UpdateLicenseActivation(licenseActivation);
            return result > 0 ? Ok("License activation updated") : BadRequest("Failed to update license activation");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _licenseActivationService.DeleteLicenseActivation(id);
            return result > 0 ? Ok("License activation deleted") : BadRequest("Failed to delete license activation");
        }
    }
}
