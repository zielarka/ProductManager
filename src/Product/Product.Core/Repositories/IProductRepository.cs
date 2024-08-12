using ProductManager.Core.Entities;

namespace ProductManager.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> CreateProductAsync(Product ProductManager);
    }
}
