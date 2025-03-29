
namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/blacklisted")]
    public class BlackListedController : ControllerBase
    {
        private readonly IBlackListedService _blackListedService;

        public BlackListedController(IBlackListedService blackListedService)
        {
            _blackListedService = blackListedService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlackListed blackListed)
        {
            blackListed.CreatedAt = DateTime.UtcNow;
            blackListed.LastUpdateAt = DateTime.UtcNow;

            var result = await _blackListedService.CreateBlackListed(blackListed);
            return result > 0 ? Ok("Blacklisted entry created") : BadRequest("Failed to create blacklisted entry");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var blackListed = await _blackListedService.GetBlackListedById(id);
            return blackListed != null ? Ok(blackListed) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blackListedEntries = await _blackListedService.GetAllBlackListed();
            return Ok(blackListedEntries);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlackListed blackListed)
        {
            blackListed.Id = id;
            blackListed.LastUpdateAt = DateTime.UtcNow;

            var result = await _blackListedService.UpdateBlackListed(blackListed);
            return result > 0 ? Ok("Blacklisted entry updated") : BadRequest("Failed to update blacklisted entry");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blackListedService.DeleteBlackListed(id);
            return result > 0 ? Ok("Blacklisted entry deleted") : BadRequest("Failed to delete blacklisted entry");
        }
    }
}
