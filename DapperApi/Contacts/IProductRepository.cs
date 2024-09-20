using DapperApi.Models;

namespace DapperApi.Contacts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(int id);
    }
}
