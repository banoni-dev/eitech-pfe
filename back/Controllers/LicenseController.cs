
namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/licenses")]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] License license)
        {
            license.CreatedAt = DateTime.UtcNow;
            license.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseService.CreateLicense(license);
            return result > 0 ? Ok("License created") : BadRequest("Failed to create license");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var license = await _licenseService.GetLicenseById(id);
            return license != null ? Ok(license) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var licenses = await _licenseService.GetAllLicenses();
            return Ok(licenses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] License license)
        {
            license.LicenseId = id;
            license.LastUpdateAt = DateTime.UtcNow;

            var result = await _licenseService.UpdateLicense(license);
            return result > 0 ? Ok("License updated") : BadRequest("Failed to update license");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _licenseService.DeleteLicense(id);
            return result > 0 ? Ok("License deleted") : BadRequest("Failed to delete license");
        }
    }
}
