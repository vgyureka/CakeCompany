using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider;
using Moq;
using NUnit.Framework;
using System;

namespace CakeCompany.UnitTest.Providers
{
    public class PaymentProviderTest
    {
        private PaymentProvider _paymentProvider;
        private Mock<PaymentIn> _paymentIn;
        [SetUp]
        public void SetUp()
        {
            _paymentProvider = new PaymentProvider();
            _paymentIn = new Mock<PaymentIn>();
        }

        [Test]
        public void GetLatestOrders_Test()
        {
            //Assign
            Order order = new Order("Important", "cakebox@gmail.com", DateTime.Now.Add(TimeSpan.FromMinutes(90)), 1, Cake.Chocolate, 1, 120.25);
            var expected = _paymentIn.Object.HasCreditLimit = false;
            //Act
            PaymentIn actual = _paymentProvider.Process(order);
            //Assert
            Assert.AreEqual(expected, actual.HasCreditLimit);
        }

    }
}
