using CakeCompany.Models;

namespace CakeCompany.Provider.Interfaces
{
    public interface IOrderProvider
    {
        public Order[] GetLatestOrders();
        public void UpdateOrders(Order[] orders);
    }
}
