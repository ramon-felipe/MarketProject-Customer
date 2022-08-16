using MarketProject.CustomerService.Domain.Entities;
using MarketProject.CustomerService.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.CQRS.Queries
{
    public class GetLastCustomerQuery : IGetLastCustomerQuery
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetLastCustomerQuery> _logger;

        public GetLastCustomerQuery(ILogger<GetLastCustomerQuery> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public Customer Execute()
        {
            _logger.LogInformation("Getting last user");

            var lastUser = _customerRepository.GetLast();

            _logger.LogInformation("Last user gotten!");

            return lastUser;
        }
    }
}
