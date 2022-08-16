using MarketProject.CustomerService.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.CQRS.Commands
{
    public class CreateCustomerCommand : ICreateCustomerCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommand> _logger;

        public CreateCustomerCommand(ICustomerRepository customerRepository, ILogger<CreateCustomerCommand> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public void Execute(string name)
        {
            _logger.LogInformation("Creating customer...");
            _customerRepository.Create(name);
            _logger.LogInformation("Customer created!");
        }
    }
}
