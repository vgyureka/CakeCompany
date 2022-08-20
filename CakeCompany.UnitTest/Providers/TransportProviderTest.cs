using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CakeCompany.UnitTest.Providers
{
    public class TransportProviderTest
    {
        private TransportProvider _transportProvider;
        private Mock<Product> _productMock;
        [SetUp]
        public void SetUp()
        {
            _transportProvider = new TransportProvider();
            _productMock = new Mock<Product>();
        }

        [Test]
        public void CheckForAvailability_totalQuantityIsLessThanOneThousand()
        {
            //Assign
            List<Product> products = new List<Product>
            {
                new Product { Quantity=5 },
                new Product { Quantity=50 }
            };
            var expected = TransportType.Van;
            //Act
            var transportType = _transportProvider.CheckForAvailability(products);
            //Assert
            Assert.AreEqual(expected, transportType);
        }

        [Test]
        public void CheckForAvailability_totalQuantityIsBetweenOneAndFiveThousand()
        {
            //Assign
            List<Product> products = new List<Product>
            {
                new Product { Quantity=500 },
                new Product { Quantity=1000 }
            };
            var expected = TransportType.Truck;
            //Act
            var transportType = _transportProvider.CheckForAvailability(products);
            //Assert
            Assert.AreEqual(expected, transportType);
        }

        [Test]
        public void CheckForAvailability_totalQuantityIsMoreThanFiveThousand()
        {
            //Assign
            List<Product> products = new List<Product>
            {
                new Product { Quantity=4000 },
                new Product { Quantity=2000 }
            };
            var expected = TransportType.Ship;
            //Act
            var transportType = _transportProvider.CheckForAvailability(products);
            //Assert
            Assert.AreEqual(expected, transportType);
        }
    }
}
