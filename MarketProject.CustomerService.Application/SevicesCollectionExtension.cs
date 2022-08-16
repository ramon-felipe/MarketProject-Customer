using MarketProject.CustomerService.Application.Customers;
using MarketProject.CustomerService.Application.Customers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Application
{
    public static class SevicesCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerApplication, CustomerApplication>();
        }
    }
}
