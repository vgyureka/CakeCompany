using CakeCompany.Models;
using CakeCompany.Models.Enums;
using System.Collections.Generic;

namespace CakeCompany.Provider.Interfaces
{
    public interface ITransportProvider
    {
        public TransportType CheckForAvailability(List<Product> products);
    }
}