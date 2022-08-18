using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain;
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
    public class GetCustomerQuery : IGetCustomerQuery
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetCustomerQuery> _logger;

        public GetCustomerQuery(ILogger<GetCustomerQuery> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public Result<Customer> Execute(int customerId)
        {
            _logger.LogInformation("Getting customer...");
            var result = _customerRepository.Get(customerId);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Customer found!");

                return result;
            }

            var errorMsg = $"It was not possible to locate the customer ID {customerId}.";
            _logger.LogError(errorMsg);

            if(result.Error.InnerException is InvalidOperationException)
                return Result.Failure<Customer>(ResultError.Create(errorMsg));

            throw result.Error;
        }
    }
}
