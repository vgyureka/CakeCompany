using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider;
using Moq;
using NUnit.Framework;
using System;

namespace CakeCompany.UnitTest.Providers
{
    public class CakeProviderTest
    {
        private CakeProvider _cakeProvider;
        private Mock<Product> _product;
        [SetUp]
        public void SetUp()
        {
            _cakeProvider = new CakeProvider();
            _product = new Mock<Product>();
        }

        [Test]
        public void Check_Test()
        {
            //Assign
            Order order = new Order("CakeBox", "cakebox@gmail.com", DateTime.Now.Add(TimeSpan.FromMinutes(90)), 1, Cake.Chocolate, 1, 120.25);
            var expected = DateTime.Now.Add(TimeSpan.FromMinutes(30));
            //Act
            DateTime actual = _cakeProvider.Check(order);
            //Assert
            Assert.AreEqual(expected.Minute,actual.Minute);
            Assert.AreEqual(expected.Hour,actual.Hour);
            Assert.AreEqual(expected.Date,actual.Date);
        }

        [Test]
        public void Bake_Test()
        {
            //Assign
            Order order = new Order("CakeBox", "cakebox@gmail.com", DateTime.Now.Add(TimeSpan.FromMinutes(90)), 1, Cake.Chocolate, 1, 120.25);
            _product.Object.Cake = Cake.Chocolate;
            _product.Object.Quantity = order.Quantity;
            //Act
            var actual = _cakeProvider.Bake(order);
            //Assert
            Assert.AreEqual(_product.Object.Cake, actual.Cake);
            Assert.AreEqual(_product.Object.Quantity, actual.Quantity);

        }
    }
}
