using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain.Models;

namespace MarketProject.CustomerService.Application.Customers.Interfaces
{
    public interface ICustomerApplication
    {
        Result<CustomerResponseModel> GetCustomer(int customerId);
        Result<IEnumerable<CustomerResponseModel>> GetAllCustomers();
        Task<HttpResponseMessage> GetCustomerAccount();
        CustomerResponseModel CreateAndReturnCustomer(string name);
    }
}
