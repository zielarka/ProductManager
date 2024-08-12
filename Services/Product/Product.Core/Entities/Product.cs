namespace ProductManager.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; protected set; }
        public string Code { get; protected set; }
        public decimal Price { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Product(Guid id, string name, string code, decimal price, DateTime createdAt)
        {
            Id = id;
            SetName(name);
            SetCode(code);
            Price = price;
            CreatedAt = createdAt;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Product name can not be empty.");
            }

            Name = name.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new Exception("Product code can not be empty.");
            }

            Code = code.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
