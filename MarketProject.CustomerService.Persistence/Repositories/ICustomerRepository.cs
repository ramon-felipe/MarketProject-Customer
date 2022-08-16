using MarketProject.CustomerService.Domain;
using MarketProject.CustomerService.Domain.Entities;

namespace MarketProject.CustomerService.Persistence.Repositories
{
    public interface ICustomerRepository
    {
        Result<Customer> Get(int customerId);
        Customer GetLast();
        IEnumerable<Customer> GetAll();
        Customer Create(string name);
    }
}