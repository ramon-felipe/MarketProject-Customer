using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain;
using MarketProject.CustomerService.Domain.Entities;

namespace MarketProject.CustomerService.Persistence.Repositories
{
    public interface ICustomerRepository
    {
        Result<Customer> Get(int customerId);
        Result<IEnumerable<Customer>> GetAll();
        Customer GetLast();
        Customer Create(string name);
    }
}