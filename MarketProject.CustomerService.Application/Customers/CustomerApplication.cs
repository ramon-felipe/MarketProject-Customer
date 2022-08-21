using AutoMapper;
using MarketProject.CustomerService.Application.Customers.Interfaces;
using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Common.Extensions;
using MarketProject.CustomerService.Domain;
using MarketProject.CustomerService.Domain.Models;
using MarketProject.CustomerService.Persistence.CQRS.Commands.Interfaces;
using MarketProject.CustomerService.Persistence.CQRS.Queries.Interfaces;
using MarketProject.CustomerService.Services.MessageService.Interfaces;
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
        private readonly IGetAllCustomersQuery _getAllCustomersQuery;
        
        private readonly IMessageService _rabbitMq;

        public CustomerApplication(IHttpClientFactory httpClientFactory, 
                                   ILogger<CustomerApplication> logger,
                                   IMapper mapper,
                                   ICreateCustomerCommand createCustomerCommand, 
                                   IGetCustomerQuery getCustomerQuery,
                                   IGetLastCustomerQuery getLastCustomerQuery,
                                   IGetAllCustomersQuery getAllCustomersQuery,
                                   IMessageService rabbitMq)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _mapper = mapper;
            _createCustomerCommand = createCustomerCommand;
            _getCustomerQuery = getCustomerQuery;
            _getLastCustomerQuery = getLastCustomerQuery;
            _getAllCustomersQuery = getAllCustomersQuery;
            _rabbitMq = rabbitMq;
        }

        public CustomerResponseModel CreateAndReturnCustomer(string name)
        {
            _createCustomerCommand.Execute(name);

            var customer = _getLastCustomerQuery.Execute();
            var customerResult = _mapper.Map<CustomerResponseModel>(customer);

            return customerResult;
        }

        public Result<IEnumerable<CustomerResponseModel>> GetAllCustomers()
        {
            var result = _getAllCustomersQuery.Execute().Value;
            var customersResult = Result.Success(_mapper.Map<IEnumerable<CustomerResponseModel>>(result));

            return customersResult;
        }

        public Result<CustomerResponseModel> GetCustomer(int customerId)
        {
            var result = _getCustomerQuery.Execute(customerId);

            if (result.IsFailure)
                return Result.Failure<CustomerResponseModel>(result.Error);

            var customer = result.Value;
            var customerResult = Result.Success(_mapper.Map<CustomerResponseModel>(customer));

            var message = customer.Serialize().Value.DeSerializeText();

            _rabbitMq.SendMessage("testExchange", "testQueue", "ppp", message);

            return customerResult;
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