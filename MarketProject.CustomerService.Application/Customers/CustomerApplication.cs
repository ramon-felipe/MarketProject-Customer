using AutoMapper;
using MarketProject.CustomerService.Application.Customers.Interfaces;
using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Domain;
using MarketProject.CustomerService.Domain.Models;
using MarketProject.CustomerService.Persistence.CQRS.Commands;
using MarketProject.CustomerService.Persistence.CQRS.Queries;
using Microsoft.Extensions.Logging;

namespace MarketProject.CustomerService.Application.Customers
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerApplication> _logger;
        private readonly IMapper _mapper;

        private readonly ICreateCustomerCommand _createCustomerCommand;
        private readonly IGetCustomerQuery _getCustomerQuery;
        private readonly IGetLastCustomerQuery _getLastCustomerQuery;

        public CustomerApplication(IHttpClientFactory httpClientFactory, 
                                   ILogger<CustomerApplication> logger,
                                   IMapper mapper,
                                   ICreateCustomerCommand createCustomerCommand, 
                                   IGetCustomerQuery getCustomerQuery,
                                   IGetLastCustomerQuery getLastCustomerQuery)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _mapper = mapper;
            _createCustomerCommand = createCustomerCommand;
            _getCustomerQuery = getCustomerQuery;
            _getLastCustomerQuery = getLastCustomerQuery;
        }

        public CustomerResponseModel CreateAndReturnCustomer(string name)
        {
            _createCustomerCommand.Execute(name);

            var customer = _getLastCustomerQuery.Execute();
            var customerResult = _mapper.Map<CustomerResponseModel>(customer);

            return customerResult;
        }

        public Result<CustomerResponseModel> GetCustomer(int customerId)
        {
            var result = _getCustomerQuery.Execute(customerId);

            if (result.IsSuccess)
            {
                var customer = result.Value;
                var customerResult = Result.Success(_mapper.Map<CustomerResponseModel>(customer));

                return customerResult;
            }

            return Result.Failure<CustomerResponseModel>(result.Error);
        }

        public Task<HttpResponseMessage> GetCustomerAccount()
        {
            _logger.LogInformation("Calling GetCustomerAccount...");

            var httpClient = _httpClientFactory.CreateClient("CustomerAccount");
            var result = httpClient.GetAsync("CustomerAccount");

            var baseAddress = httpClient.BaseAddress;
            _logger.LogInformation("BaseAddress: {0}", baseAddress);
            _logger.LogInformation("GetCustomerAccount called.");

            return result;
        }
    }
}