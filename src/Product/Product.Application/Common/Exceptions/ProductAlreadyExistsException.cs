namespace ProductManager.Application.Common.Exceptions
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string name)
            : base($"Product with name: '{name}' already exists")
        {
        }
    }
}
