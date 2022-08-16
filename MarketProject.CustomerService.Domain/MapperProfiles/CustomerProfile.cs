using AutoMapper;
using MarketProject.CustomerService.Domain.Entities;
using MarketProject.CustomerService.Domain.Models;

namespace MarketProject.CustomerService.Domain.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseModel>();
        }
    }
}
