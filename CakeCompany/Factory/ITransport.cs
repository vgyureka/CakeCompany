using CakeCompany.Models;
using System.Collections.Generic;

namespace CakeCompany.Factory
{
    public interface ITransport
    {
        public bool Deliver(List<Product> products);
    }
}
