using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductManager.Application.Common.Exceptions;

namespace ProductManager.Application.Common.Behaviors
{
    public class RequestExceptionHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestExceptionHandler<TRequest, TResponse>> _logger;

        public RequestExceptionHandler(ILogger<RequestExceptionHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (ProductAlreadyExistsException ex)
            {
                _logger.LogError(ex, "Product already exists");
                throw new HttpResponseException(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new HttpResponseException(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
