using Microsoft.AspNetCore.Mvc;

namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.LastUpdateAt = DateTime.UtcNow;

            var result = await _userService.CreateUser(user);
            return result > 0 ? Ok("User created") : BadRequest("Failed to create user");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            user.UserId = id;
            user.LastUpdateAt = DateTime.UtcNow;

            var result = await _userService.UpdateUser(user);
            return result > 0 ? Ok("User updated") : BadRequest("Failed to update user");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);
            return result > 0 ? Ok("User deleted") : BadRequest("Failed to delete user");
        }
    }
}
