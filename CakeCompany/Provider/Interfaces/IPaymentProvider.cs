using CakeCompany.Models;

namespace CakeCompany.Provider.Interfaces
{
    public interface IPaymentProvider
    {
        PaymentIn Process(Order order);
    }
}
