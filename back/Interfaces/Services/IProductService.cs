namespace EitechPfe.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateProduct(Product product);
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(int id);
    }
}
