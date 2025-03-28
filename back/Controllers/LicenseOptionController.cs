using Microsoft.AspNetCore.Mvc;
using EitechPfe.Interfaces;
using EitechPfe.Entities;

[ApiController]
[Route("api/license-options")]
public class LicenseOptionController : ControllerBase
{
    private readonly ILicenseOptionService _licenseOptionService;

    public LicenseOptionController(ILicenseOptionService licenseOptionService)
    {
        _licenseOptionService = licenseOptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LicenseOption licenseOption)
    {
        licenseOption.CreatedAt = DateTime.UtcNow;
        licenseOption.LastUpdateAt = DateTime.UtcNow;

        var result = await _licenseOptionService.CreateLicenseOption(licenseOption);
        return result > 0 ? Ok("License option created") : BadRequest("Failed to create license option");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var licenseOption = await _licenseOptionService.GetLicenseOptionById(id);
        return licenseOption != null ? Ok(licenseOption) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var licenseOptions = await _licenseOptionService.GetAllLicenseOptions();
        return Ok(licenseOptions);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LicenseOption licenseOption)
    {
        licenseOption.OptionId = id;
        licenseOption.LastUpdateAt = DateTime.UtcNow;

        var result = await _licenseOptionService.UpdateLicenseOption(licenseOption);
        return result > 0 ? Ok("License option updated") : BadRequest("Failed to update license option");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _licenseOptionService.DeleteLicenseOption(id);
        return result > 0 ? Ok("License option deleted") : BadRequest("Failed to delete license option");
    }
}
