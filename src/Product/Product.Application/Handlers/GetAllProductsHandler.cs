using ProductManager.Application.Mappers;
using ProductManager.Application.Queries;
using ProductManager.Application.Responses;
using ProductManager.Core.Repositories;
using MediatR;

namespace ProductManager.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _productRepository.GetProductsAsync();
            var productsResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productsList);
            return productsResponseList;
        }
    }
}
