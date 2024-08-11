using ProductManager.Core.Repositories;
using ProductManager.Core.Entities;

namespace ProductManager.Application.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Product> GetOrFailAsyncIfExists(this IProductRepository repository, string name)
        {
            var product = await repository.GetProductByNameAsync(name);
            if (product != null)
            {
                throw new Exception($"Product with name: '{name}' already exists");
            }
            return product;
        }
    }
}
