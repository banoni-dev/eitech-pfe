using EitechPfe.Modules.Product.Interfaces;

namespace EitechPfe.Modules.Product
{
    public class ProductRepository : EitechPfe.Modules.Product.Interfaces.IProductRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public ProductRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateProduct(Product product)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                INSERT INTO products (name, description, product_type, created_at, updated_at, is_archived)
                VALUES (@Name, @Description, @ProductType, @CreatedAt, @UpdatedAt, @IsArchived);";
            var dbProduct = new
            {
                product.Name,
                product.Description,
                ProductType = product.ProductType == ProductType.License ? "License" : "Subscription",
                product.CreatedAt,
                product.UpdatedAt,
                product.IsArchived
            };
            return await connection.ExecuteAsync(sql, dbProduct);
        }

        public async Task<Product?> GetProductById(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    id AS Id,
                    name AS Name,
                    description AS Description,
                    product_type AS ProductType,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt,
                    is_archived AS IsArchived
                FROM products
                WHERE id = @Id AND is_archived = FALSE;";
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                SELECT 
                    id AS Id,
                    name AS Name,
                    description AS Description,
                    product_type AS ProductType,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt,
                    is_archived AS IsArchived
                FROM products
                WHERE is_archived = FALSE;";
            return await connection.QueryAsync<Product>(sql);
        }

        public async Task<int> UpdateProduct(Product product)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = @"
                UPDATE products
                SET name = @Name, description = @Description, product_type = @ProductType, 
                    updated_at = @UpdatedAt, is_archived = @IsArchived
                WHERE id = @Id;";
            var dbProduct = new
            {
                product.Id,
                product.Name,
                product.Description,
                ProductType = product.ProductType == ProductType.License ? "License" : "Subscription",
                product.UpdatedAt,
                product.IsArchived
            };
            return await connection.ExecuteAsync(sql, dbProduct);
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "UPDATE products SET is_archived = TRUE WHERE id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
