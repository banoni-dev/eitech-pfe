using EitechPfe.DTOs.Requests;
using EitechPfe.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid admin request.");

            var result = await _adminService.CreateAdmin(request);
            return result > 0 ? Ok("Admin created successfully.") : BadRequest("Failed to create admin.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            return admin != null ? Ok(admin) : NotFound("Admin not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminService.GetAllAdmins();
            return Ok(admins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdminRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid admin request.");

            var result = await _adminService.UpdateAdmin(id, request);
            return result > 0 ? Ok("Admin updated successfully.") : BadRequest("Failed to update admin.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminService.DeleteAdmin(id);
            return result > 0 ? Ok("Admin deleted successfully.") : BadRequest("Failed to delete admin.");
        }
    }
}
