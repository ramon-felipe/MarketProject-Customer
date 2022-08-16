using MarketProject.CustomerService.Common;

namespace MarketProject.CustomerService.Domain.ValueObjects
{  
    public record struct CustomerName
    {
        public string Name { get; set; } = string.Empty;

        private CustomerName(string name)
        {
            Name = name;
        }

        public static Result<CustomerName> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<CustomerName>(ResultError.Create("Name is null or empty."));

            if (name.Length <= 2)
                return Result.Failure<CustomerName>(ResultError.Create("Name is too short."));

            if (name.Length > 100)
                return Result.Failure<CustomerName>(ResultError.Create("Name is too long."));

            return Result.Success(new CustomerName(name));
        }
    }
}
