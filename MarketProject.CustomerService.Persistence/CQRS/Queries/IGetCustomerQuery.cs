using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain.Entities;

namespace MarketProject.CustomerService.Persistence.CQRS.Queries
{
    public interface IGetCustomerQuery
    {
        Result<Customer> Execute(int customerId); 
    }
}
