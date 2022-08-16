using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Persistence.CQRS.Commands
{
    public interface ICreateCustomerCommand
    {
        void Execute(string name);
    }
}
