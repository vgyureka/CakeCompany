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
                .AddSingleton(Log.Logger)
                .AddTransient<IOrderProvider, OrderProvider>()
                .AddTransient<IShipmentService, ShipmentService>()
                .AddTransient<ICakeProvider, CakeProvider>()
                .AddTransient<IPaymentProvider, PaymentProvider>()
                .AddTransient<ITransportProvider, TransportProvider>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
