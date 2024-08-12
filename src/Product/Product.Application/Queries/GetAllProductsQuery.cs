using ProductManager.Application.Responses;
using MediatR;

namespace ProductManager.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
    {
    }
}
