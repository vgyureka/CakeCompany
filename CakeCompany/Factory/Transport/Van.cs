using CakeCompany.Factory;
using CakeCompany.Models;
using System.Collections.Generic;

namespace CakeCompany.Factory.Transport
{
    internal class Van : ITransport
    {
        public bool Deliver(List<Product> products)
        {
            return true;
        }
    }
}
