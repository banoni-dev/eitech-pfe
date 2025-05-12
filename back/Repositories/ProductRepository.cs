namespace EitechPfe.Repositories
{
    public class ProductRepository : IProductRepository
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
            return await connection.ExecuteAsync(sql, product);
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
                WHERE id = @Id;";
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
                FROM products;";
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
            return await connection.ExecuteAsync(sql, product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = _dbConfig.GetConnection();
            string sql = "DELETE FROM products WHERE id = @Id;";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
