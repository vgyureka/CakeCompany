using CakeCompany.Provider;
using CakeCompany.Provider.Interfaces;
using CakeCompany.Service;
using CakeCompany.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CakeCompany.Utilities
{
    public class ServiceConfiguration
    {
        public static ServiceProvider AddServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(Log.Logger)
                .AddSingleton<IOrderProvider, OrderProvider>()
                .AddSingleton<IShipmentService, ShipmentService>()
                .AddSingleton<ICakeProvider, CakeProvider>()
                .AddSingleton<IPaymentProvider, PaymentProvider>()
                .AddSingleton<ITransportProvider, TransportProvider>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
