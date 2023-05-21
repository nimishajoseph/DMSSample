using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.Business.Services.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? GetByName(string name);
    }
}
