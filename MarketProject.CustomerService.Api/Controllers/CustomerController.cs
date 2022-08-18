using MarketProject.CustomerService.Application.Customers.Interfaces;
using MarketProject.CustomerService.Domain.Entities;
using MarketProject.CustomerService.Domain.Models;
using MarketProject.CustomerService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace MarketProject.CustomerService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerApplication _customerApplication;

    public CustomerController(ILogger<CustomerController> logger, ICustomerApplication customerApplication)
    {
        _logger = logger;
        _customerApplication = customerApplication;
    }

    [HttpGet("{id}",Name = "GetCustomer")]
    public ActionResult<CustomerResponseModel> Get(int id)
    {
        var result = _customerApplication.GetCustomer(id);

        if(result.IsFailure)
            return Problem(result.Error.Message);

        var customer = result.Value;
        _logger.LogInformation($"User found: {customer}");
        return Ok(customer);
    }

    [HttpGet("getall",Name = nameof(GetAll))]
    public ActionResult<IEnumerable<CustomerResponseModel>> GetAll()
    {
        var result = _customerApplication.GetAllCustomers();

        if (result.IsFailure)
            return Problem(result.Error.Message);
        
        var customers = result.Value;
        _logger.LogInformation($"Users found: {customers.Count()}");
        return Ok(customers);
    }

    [HttpGet("getCustomerAccount" , Name = nameof(GetCustomerAccount))]
    public async Task<ActionResult<string>> GetCustomerAccount()
    {
        _logger.LogInformation($"Getting customer account...");
        var customerAccount = await _customerApplication.GetCustomerAccount();
        customerAccount.EnsureSuccessStatusCode();

        var content = customerAccount.Content.ReadAsStringAsync();

        _logger.LogInformation($"User found: {content}");
        return Ok(content);
    }

    [HttpPost(Name = "CreateCustomer")]
    public ActionResult<CustomerResponseModel> Create(CreateCustomerRequestModel request)
    {
        var customerName = request.Name;
        var customer = _customerApplication.CreateAndReturnCustomer(customerName);

        _logger.LogInformation($"User created: {customer}");
        return Ok(customer);
    }

    [HttpPut(Name = "UpdateCustomer")]
    public ActionResult<Customer> Update()
    {
        throw new NotImplementedException("It's not possible to update a customer yet.");
    }

    [HttpDelete(Name = "DeleteCustomer")]
    public ActionResult<Customer> Delete()
    {
        throw new NotImplementedException("It's not possible to delete a customer yet.");
    }
}
