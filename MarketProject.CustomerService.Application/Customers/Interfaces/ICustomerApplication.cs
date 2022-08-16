using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain.Models;

namespace MarketProject.CustomerService.Application.Customers.Interfaces
{
    public interface ICustomerApplication
    {
        Result<CustomerResponseModel> GetCustomer(int customerId);
        Task<HttpResponseMessage> GetCustomerAccount();
        CustomerResponseModel CreateAndReturnCustomer(string name);
    }
}
