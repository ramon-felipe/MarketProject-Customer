using MarketProject.CustomerService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.CQRS.Queries.Interfaces
{
    public interface IGetLastCustomerQuery
    {
        Customer Execute();
    }
}
