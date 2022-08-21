using FluentValidation;
using FluentValidation.AspNetCore;
using MarketProject.CustomerService.Application;
using MarketProject.CustomerService.Application.Exceptions.Filters;
using MarketProject.CustomerService.Domain.MapperProfiles;
using MarketProject.CustomerService.Domain.Validators;
using MarketProject.CustomerService.Persistence.CQRS.Commands;
using MarketProject.CustomerService.Persistence.CQRS.Commands.Interfaces;
using MarketProject.CustomerService.Persistence.CQRS.Queries;
using MarketProject.CustomerService.Persistence.CQRS.Queries.Interfaces;
using MarketProject.CustomerService.Persistence.Repositories;
using MarketProject.CustomerService.Services.MessageService.Interfaces;
using MarketProject.CustomerService.Services.MessageService.RabbitMQ;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = builder.Configuration.GetValue<int>("https_port");
});

builder.Services.AddServices();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCustomerRequestValidator));
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(typeof(CustomerProfile));

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICreateCustomerCommand, CreateCustomerCommand>();
builder.Services.AddScoped<IGetCustomerQuery, GetCustomerQuery>();
builder.Services.AddScoped<IGetLastCustomerQuery, GetLastCustomerQuery>();
builder.Services.AddScoped<IGetAllCustomersQuery, GetAllCustomersQuery>();

builder.Services.AddScoped<IMessageService, RabbitMqService>();

builder.Services.AddHttpClient("CustomerAccount", httpClient =>
{
    var uri = builder.Configuration.GetValue<string>("Endpoints:CustomerAccountService");
    httpClient.BaseAddress = new Uri(uri);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (req, cert, chain, sslPolicy) => true,
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
