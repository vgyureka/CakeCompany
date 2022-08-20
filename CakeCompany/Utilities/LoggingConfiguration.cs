using Serilog;

namespace CakeCompany.Service
{
    public class LoggingConfiguration
    {
        public static void AddLoggers()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        }
    }
}
