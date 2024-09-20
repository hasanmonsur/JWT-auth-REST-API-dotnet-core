using Dapper;
using DapperApi.Contacts;
using DapperApi.Data;
using DapperApi.Models;
using Microsoft.Data.SqlClient;

namespace DapperApi.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _dapperContext;

        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "SELECT * FROM Products";
                return await connection.QueryAsync<Product>(sql);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "SELECT * FROM Products WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
            }
        }

        public async Task<int> AddProduct(Product product)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
                return await connection.ExecuteAsync(sql, product);
            }
        }

        public async Task<int> UpdateProduct(Product product)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                return await connection.ExecuteAsync(sql, product);
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";
                return await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
