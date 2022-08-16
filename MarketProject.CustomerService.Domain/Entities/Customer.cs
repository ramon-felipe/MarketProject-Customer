using MarketProject.CustomerService.Common;

namespace MarketProject.CustomerService.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; private set; }

        private Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Customer> Create(int id, string name)
        {
            if (id == 0)
                return Result.Failure<Customer>(ResultError.Create("Id should not be zero."));

            if (string.IsNullOrEmpty(name))
                return Result.Failure<Customer>(ResultError.Create("Name null or empty."));
            
            if (name.Length <= 2)
                return Result.Failure<Customer>(ResultError.Create("Name is too short."));
            
            if (name.Length > 100)
                return Result.Failure<Customer>(ResultError.Create("Name is too long."));

            return Result.Success(new Customer(id, name));
        }

        public override string ToString()
            => $"User id: {Id}, user name: {Name}";
    }
}