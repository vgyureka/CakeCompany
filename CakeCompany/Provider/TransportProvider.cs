using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CakeCompany.Provider
{
    public class TransportProvider : ITransportProvider
    {
        private double _minQuantity;
        private double _maxQuantity;

        public TransportProvider()
        {
            _minQuantity = 1000;
            _maxQuantity = 5000;
        }
        public TransportType CheckForAvailability(List<Product> products)
        {
            double totalQuantity = products.Sum(p => p.Quantity);

            if (totalQuantity < _minQuantity)
            {
                return TransportType.Van;
            }
            if (totalQuantity > _minQuantity && totalQuantity < _maxQuantity)
            {
                return TransportType.Truck;
            }
            return TransportType.Ship;
        }
    }
}
