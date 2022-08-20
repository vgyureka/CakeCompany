using CakeCompany.Factory.Transport;
using CakeCompany.Models.Enums;

namespace CakeCompany.Factory
{
    public class TransportFactory
    {
        static public ITransport GetTransport(TransportType transportType)
        {
            ITransport transportObject = null;
            switch (transportType)
            {
                case TransportType.Ship: transportObject = new Ship(); break;
                case TransportType.Truck: transportObject = new Truck(); break;
                case TransportType.Van: transportObject = new Van(); break;
            }
            return transportObject;
        }
    }
}
