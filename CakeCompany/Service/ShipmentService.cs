using CakeCompany.Factory;
using CakeCompany.Models;
using CakeCompany.Provider.Interfaces;
using CakeCompany.Service.Interfaces;
using CakeCompany.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeCompany.Service
{
    public class ShipmentService : IShipmentService
    {
        private readonly IOrderProvider _orderProvider;
        private readonly ICakeProvider _cakeProvider;
        private readonly IPaymentProvider _paymentProvider;
        private readonly ITransportProvider _transportProvider;
        public ShipmentService(IOrderProvider orderProvider, ICakeProvider cakeProvider, IPaymentProvider paymentProvider, ITransportProvider transportProvider)
        {
            _orderProvider = orderProvider;
            _cakeProvider = cakeProvider;
            _paymentProvider = paymentProvider;
            _transportProvider = transportProvider;
        }
        public void GetShipment()
        {
            try
            {
                //Call order to get new orders
                var orders = _orderProvider.GetLatestOrders();
               Log.Information("Got the cake orders !!");
                if (!orders.Any())
                {
                    Log.Information("No Orders found");
                    return;
                }
                var cancelledOrders = new List<Order>();
                var products = new List<Product>();
                DateTime estimatedBakeTime;
                Product product;
                foreach (var order in orders)
                {
                    estimatedBakeTime = _cakeProvider.Check(order);
                    if (estimatedBakeTime > order?.EstimatedDeliveryTime ||
                       !_paymentProvider.Process(order).IsSuccessful)
                    {
                        cancelledOrders.Add(order);
                        Log.Information($"Order for client [{order.ClientName}] is cancelled !!");
                    }
                    else
                    {
                        product = _cakeProvider.Bake(order);
                        products.Add(product);
                        Log.Information($"Order for client [{order.ClientName}] is added !!.");
                    }
                }

                if (products.Count > 0)
                {
                    var transportType = _transportProvider.CheckForAvailability(products);
                    var transport = TransportFactory.GetTransport(transportType);
                    transport.Deliver(products);
                    Log.Information($"Transport [{transportType}] is used to deliver products.");
                }

            }
            catch
            {
                throw new CustomException("Exception occurred in Shipment Service.");
            }

        }
    }
}
