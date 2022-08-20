using CakeCompany.Service;
using CakeCompany.Service.Interfaces;
using CakeCompany.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CakeCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ServiceConfiguration.AddServices();
            LoggingConfiguration.AddLoggers();

            Log.Information("Program started!");

            var shipmentProvider = serviceProvider.GetService<IShipmentService>();
            try
            {
                shipmentProvider.GetShipment();
                Log.Information("Program ended successfully...");
            }
            catch(CustomException ex)
            {
                Log.Error($"Error Message : - {ex.Message}");
            }
        }
    }
}
