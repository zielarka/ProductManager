using MediatR;
using ProductManager.Application.Commands;
using ProductManager.Application.Extensions;
using ProductManager.Application.Mappers;
using ProductManager.Application.Responses;
using ProductManager.Core.Entities;
using ProductManager.Core.Repositories;

namespace ProductManagers.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.GetOrFailAsyncIfExists(request.Name);
            var productEntity = new Product(Guid.NewGuid(), request.Name, request.Code, request.Price, DateTime.Now);
            var newProduct = await _productRepository.CreateProductAsync(productEntity);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}
