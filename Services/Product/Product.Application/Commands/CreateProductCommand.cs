using ProductManager.Application.Responses;
using MediatR;

namespace ProductManager.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }

    }
}
