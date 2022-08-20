using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider.Interfaces;
using CakeCompany.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CakeCompany.UnitTest.Services
{
    public class ShipmentServiceTests
    {
        private ShipmentService _shipmentService;
        private Mock<IOrderProvider> _orderProviderMock;
        private Mock<ICakeProvider> _cakeProviderMock;
        private Mock<IPaymentProvider> _paymentProviderMock;
        private Mock<ITransportProvider> _transportProviderMock;
        private Mock<List<Product>> _productsMock;
        private Mock<Product> _productMock;
        private Mock<PaymentIn> _paymentInMock;

        [SetUp]
        public void SetUp()
        {
            _orderProviderMock = new Mock<IOrderProvider>();
            _cakeProviderMock = new Mock<ICakeProvider>();
            _paymentProviderMock = new Mock<IPaymentProvider>();
            _transportProviderMock = new Mock<ITransportProvider>();
            _shipmentService = new ShipmentService(_orderProviderMock.Object, _cakeProviderMock.Object, _paymentProviderMock.Object, _transportProviderMock.Object);
            _productsMock = new Mock<List<Product>>();
            _productMock = new Mock<Product>();
            _paymentInMock = new Mock<PaymentIn>();
        }

        [Test]
        public void GetShipmentTest_CancelledOrders_unSuccessfulEstimatedDelivery()
        {
            // arrange
            Order[] orders = new Order[] {
            new Order ( "CakeBox", "cakebox@gmail.com",  DateTime.Now,  1,  Cake.RedVelvet,  1, 120.25 ),
            new Order ( "ImportantCakeShop", "importantCakeShop@gmail.com",  DateTime.Now,  2,  Cake.Chocolate,  3, 140.25 )
        };
            //act
            _orderProviderMock.Setup(x => x.GetLatestOrders()).Returns(orders);
            _cakeProviderMock.Setup(x => x.Check(orders[0])).Returns(DateTime.Now.AddMinutes(60));
            _cakeProviderMock.Setup(p => p.Check(orders[1])).Returns(DateTime.Now.AddMinutes(30));
            _paymentProviderMock.Setup(p => p.Process(orders[0])).Returns(_paymentInMock.Object);
            _paymentProviderMock.Setup(p => p.Process(orders[1])).Returns(_paymentInMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[0])).Returns(_productMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[1])).Returns(_productMock.Object);

            _transportProviderMock.Setup(x => x.CheckForAvailability(_productsMock.Object));
            //assert
            _shipmentService.GetShipment();

            _orderProviderMock.Verify(x => x.GetLatestOrders(), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[1]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[0]), Times.Never());
            _cakeProviderMock.Verify(x => x.Bake(orders[1]), Times.Never());
            _transportProviderMock.Verify(x => x.CheckForAvailability(_productsMock.Object), Times.Never());
        }

        [Test]
        public void GetShipmentTest_CancelledOrders_unSuccessfulPayment()
        {
            // arrange
            Order[] orders = new Order[] {
            new Order ( "CakeBox", "cakebox@gmail.com",  DateTime.Now.Add(TimeSpan.FromMinutes(90)),  1,  Cake.RedVelvet,  1, 120.25 ),
            new Order ( "ImportantCakeShop", "importantCakeShop@gmail.com", DateTime.Now.Add(TimeSpan.FromMinutes(90)),  2,  Cake.Chocolate,  3, 140.25 )
        };
            _paymentInMock.Object.IsSuccessful = false;

            _orderProviderMock.Setup(x => x.GetLatestOrders()).Returns(orders);
            _cakeProviderMock.Setup(x => x.Check(orders[0])).Returns(DateTime.Now.AddMinutes(60));
            _cakeProviderMock.Setup(p => p.Check(orders[1])).Returns(DateTime.Now.AddMinutes(30));
            _paymentProviderMock.Setup(p => p.Process(orders[0])).Returns(_paymentInMock.Object);
            _paymentProviderMock.Setup(p => p.Process(orders[1])).Returns(_paymentInMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[0])).Returns(_productMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[1])).Returns(_productMock.Object);

            _transportProviderMock.Setup(x => x.CheckForAvailability(_productsMock.Object));
            //act
            _shipmentService.GetShipment();
            //assert
            _orderProviderMock.Verify(x => x.GetLatestOrders(), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[1]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[0]), Times.Never());
            _cakeProviderMock.Verify(x => x.Bake(orders[1]), Times.Never());
            _transportProviderMock.Verify(x => x.CheckForAvailability(_productsMock.Object), Times.Never());
        }

        [Test]
        public void GetShipmentTest_ValidOrders()
        {
            // arrange
            Order[] orders = new Order[] {
            new Order ( "CakeBox", "cakebox@gmail.com",  DateTime.Now.Add(TimeSpan.FromMinutes(90)),  1,  Cake.RedVelvet,  1, 120.25 ),
            new Order ( "ImportantCakeShop", "importantCakeShop@gmail.com",  DateTime.Now.Add(TimeSpan.FromMinutes(50)),  2,  Cake.Chocolate,  3, 140.25 )
        };
            _paymentInMock.Object.IsSuccessful = true;

            _orderProviderMock.Setup(x => x.GetLatestOrders()).Returns(orders);
            _cakeProviderMock.Setup(x => x.Check(orders[0])).Returns(DateTime.Now.AddMinutes(60));
            _cakeProviderMock.Setup(p => p.Check(orders[1])).Returns(DateTime.Now.AddMinutes(30));
            _paymentProviderMock.Setup(p => p.Process(orders[0])).Returns(_paymentInMock.Object);
            _paymentProviderMock.Setup(p => p.Process(orders[1])).Returns(_paymentInMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[0])).Returns(_productMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[1])).Returns(_productMock.Object);

            _transportProviderMock.Setup(x => x.CheckForAvailability(_productsMock.Object)).Returns(TransportType.Van);
            //act
            _shipmentService.GetShipment();
            //assert 
            _orderProviderMock.Verify(x => x.GetLatestOrders(), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[1]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[1]), Times.Once());
            _transportProviderMock.Verify(x => x.CheckForAvailability(It.IsAny<List<Product>>()), Times.Once());
        }

        [Test]
        public void GetShipmentTest_ValidOrderAndCancelledOrder()
        {
            // arrange
            Order[] orders = new Order[] {
            new Order ( "CakeBox", "cakebox@gmail.com",  DateTime.Now.Add(TimeSpan.FromMinutes(90)),  1,  Cake.RedVelvet,  1, 120.25 ),
            new Order ( "ImportantCakeShop", "importantCakeShop@gmail.com",  DateTime.Now.Add(TimeSpan.FromMinutes(30)),  2,  Cake.Chocolate,  3, 140.25 )
        };
            _paymentInMock.Object.IsSuccessful = true;

            _orderProviderMock.Setup(x => x.GetLatestOrders()).Returns(orders);
            _cakeProviderMock.Setup(x => x.Check(orders[0])).Returns(DateTime.Now.AddMinutes(60));
            _cakeProviderMock.Setup(p => p.Check(orders[1])).Returns(DateTime.Now.AddMinutes(30));
            _paymentProviderMock.Setup(p => p.Process(orders[0])).Returns(_paymentInMock.Object);
            _paymentProviderMock.Setup(p => p.Process(orders[1])).Returns(_paymentInMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[0])).Returns(_productMock.Object);
            _cakeProviderMock.Setup(p => p.Bake(orders[1])).Returns(_productMock.Object);

            _transportProviderMock.Setup(x => x.CheckForAvailability(_productsMock.Object));
            //act
            _shipmentService.GetShipment();
            //assert
            _orderProviderMock.Verify(x => x.GetLatestOrders(), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Check(orders[1]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[0]), Times.Once());
            _cakeProviderMock.Verify(x => x.Bake(orders[1]), Times.Never());
            _transportProviderMock.Verify(x => x.CheckForAvailability(It.IsAny<List<Product>>()), Times.Once());
        }

        [Test]
        public void GetShipmentTest_NoOrders()
        {
            // arrange
            Order[] orders = new Order[] { };
            _orderProviderMock.Setup(x => x.GetLatestOrders()).Returns(orders);
            //act
            _shipmentService.GetShipment();
            //assert
            _orderProviderMock.Verify(x => x.GetLatestOrders(), Times.Once());

        }
    }
}
