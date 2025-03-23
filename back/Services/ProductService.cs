public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<int> CreateProduct(Product product) => _productRepository.CreateProduct(product);
    public Task<Product?> GetProductById(int id) => _productRepository.GetProductById(id);
    public Task<IEnumerable<Product>> GetAllProducts() => _productRepository.GetAllProducts();
    public Task<int> UpdateProduct(Product product) => _productRepository.UpdateProduct(product);
    public Task<int> DeleteProduct(int id) => _productRepository.DeleteProduct(id);
}
