
namespace EitechPfe.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            var result = await _productService.CreateProduct(product);
            return result > 0 ? Ok("Product created") : BadRequest("Failed to create product");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            product.UpdatedAt = DateTime.UtcNow;

            var result = await _productService.UpdateProduct(product);
            return result > 0 ? Ok("Product updated") : BadRequest("Failed to update product");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return result > 0 ? Ok("Product deleted") : BadRequest("Failed to delete product");
        }
    }
}
