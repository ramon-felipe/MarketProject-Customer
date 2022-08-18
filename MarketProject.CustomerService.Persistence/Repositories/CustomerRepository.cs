using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain;
using MarketProject.CustomerService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IEnumerable<Customer> Customers { get; set; }

        public CustomerRepository()
        {
            var c1 = Customer.Create(1, "Ramon").Value;
            var c2 = Customer.Create(2, "Nathalia").Value;
            var c3 = Customer.Create(3, "John").Value;

            Customers = new List<Customer> { c1, c2, c3 };
        }

        public Result<Customer> Get(int customerId)
        {
            try
            {
                var customer = Customers.First(c => c.Id == customerId);
                return Result.Success(customer);
            }
            catch (InvalidOperationException e)
            {
                return Result.Failure<Customer>(ResultError.Create(e.Message, e));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<IEnumerable<Customer>> GetAll() 
        {
            try
            {
                return Result.Success(Customers);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetLast()
        {
            var lastUser = Customers.Last();
            return lastUser;
        }

        public Customer Create(string name)
        {
            var lastId = Customers.Last().Id;
            var newId = lastId + 1;
            var newCustomer = Customer.Create(newId, name).Value;

            var newCustomersList = Customers.Append(newCustomer);
            Customers = newCustomersList;

            return newCustomer;
        }
    }
}
