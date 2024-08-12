using ProductManager.Core.Entities;
using ProductManager.Core.Repositories;

namespace ProductManager.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ISet<Product> _products = new HashSet<Product>();

        public async Task<IEnumerable<Product>> GetProductsAsync()
            => await Task.FromResult(_products);

        public async Task<Product> GetProductByNameAsync(string name)
            => await Task.FromResult(_products.SingleOrDefault(x => x.Name == name.ToLowerInvariant()));

        public async Task<Product> CreateProductAsync(Product product)
        {
            _products.Add(product);
            return await Task.FromResult(product);
        }
    }
}
