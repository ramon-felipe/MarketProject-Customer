using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain.Entities;
using MarketProject.CustomerService.Persistence.CQRS.Queries.Interfaces;
using MarketProject.CustomerService.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.CQRS.Queries
{
    public class GetAllCustomersQuery : IGetAllCustomersQuery
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetAllCustomersQuery> _logger;

        public GetAllCustomersQuery(ICustomerRepository customerRepository, ILogger<GetAllCustomersQuery> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public Result<IEnumerable<Customer>> Execute()
        {
            _logger.LogInformation("Getting all customers...");
            var result = _customerRepository.GetAll();

            if (result.IsSuccess)
            {
                _logger.LogInformation("All customers returned!");
                return Result.Success(result.Value);
            }

            return Result.Failure<IEnumerable<Customer>>(ResultError.Create("It was not possible to get all customers."));
        }
        
    }
}
