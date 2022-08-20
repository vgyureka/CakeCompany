using CakeCompany.Provider;
using NUnit.Framework;

namespace CakeCompany.UnitTest.Providers
{
    public class OrderProviderTest
    {
        private OrderProvider _orderProvider;
        [SetUp]
        public void SetUp()
        {
            _orderProvider = new OrderProvider();
        }

        [Test]
        public void GetLatestOrders_Test()
        {
            //Act
            var actual = _orderProvider.GetLatestOrders();
            //Assert
            Assert.That(actual.Length > 0,"Orders are not available");
        }

    }
}
