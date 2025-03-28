using Microsoft.AspNetCore.Mvc;
using EitechPfe.Interfaces;
using EitechPfe.Entities;

[ApiController]
[Route("api/license-bundles")]
public class LicenseBundleController : ControllerBase
{
    private readonly ILicenseBundleService _licenseBundleService;

    public LicenseBundleController(ILicenseBundleService licenseBundleService)
    {
        _licenseBundleService = licenseBundleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LicenseBundle licenseBundle)
    {
        licenseBundle.CreatedAt = DateTime.UtcNow;
        licenseBundle.LastUpdateAt = DateTime.UtcNow;

        var result = await _licenseBundleService.CreateLicenseBundle(licenseBundle);
        return result > 0 ? Ok("License bundle created") : BadRequest("Failed to create license bundle");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var licenseBundle = await _licenseBundleService.GetLicenseBundleById(id);
        return licenseBundle != null ? Ok(licenseBundle) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var licenseBundles = await _licenseBundleService.GetAllLicenseBundles();
        return Ok(licenseBundles);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LicenseBundle licenseBundle)
    {
        licenseBundle.LicenseOrderId = id;
        licenseBundle.LastUpdateAt = DateTime.UtcNow;

        var result = await _licenseBundleService.UpdateLicenseBundle(licenseBundle);
        return result > 0 ? Ok("License bundle updated") : BadRequest("Failed to update license bundle");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _licenseBundleService.DeleteLicenseBundle(id);
        return result > 0 ? Ok("License bundle deleted") : BadRequest("Failed to delete license bundle");
    }
}
