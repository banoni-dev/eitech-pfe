using EitechPfe.Modules.Product.Interfaces;

namespace EitechPfe.Modules.Product
{
    public class ProductService : EitechPfe.Modules.Product.Interfaces.IProductService
    {
        private readonly EitechPfe.Modules.Product.Interfaces.IProductRepository _productRepository;

        public ProductService(EitechPfe.Modules.Product.Interfaces.IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> CreateProduct(Product product)
        {
            return await _productRepository.CreateProduct(product);
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}
