using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCartApi.Api.Controllers;
using ShoppingCartApi.Common.Exceptions;
using ShoppingCartApi.Common.Interface;
using ShoppingCartApi.Common.Models;
using Xunit;

namespace ShoppingCartApi.UnitTests.ControllerTests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderAccess> _mockOrderAccess = new Mock<IOrderAccess>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetReturnsOkayWhenOrdersAreFound()
        {
            List<Order> orders = _fixture.CreateMany<Order>(10).ToList();
            _mockOrderAccess.Reset();
            _mockOrderAccess.Setup(a => a.GetAllOrders()).Returns(orders);
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.Get();

            _mockOrderAccess.Verify(a => a.GetAllOrders(), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(orders.Count, ((IReadOnlyCollection<Order>)((OkObjectResult)result).Value).Count);
        }

        [Fact]
        public void GetReturns404WhenNoOrdersFound()
        {
            _mockOrderAccess.Reset();
            _mockOrderAccess.Setup(a => a.GetAllOrders()).Returns(new List<Order>());
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.Get();

            _mockOrderAccess.Verify(a => a.GetAllOrders(), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetOrderReturnsOkayWhenOrderIsFound()
        {
            Order order = _fixture.Create<Order>();
            _mockOrderAccess.Reset();
            _mockOrderAccess.Setup(a => a.GetOrder(order.Id)).Returns(order);
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.Get(order.Id);

            _mockOrderAccess.Verify(a => a.GetOrder(order.Id), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order.Id, ((Order)((OkObjectResult)result).Value).Id);
        }

        [Fact]
        public void GetOrderReturns404WhenOrderNotFound()
        {
            _mockOrderAccess.Reset();
            _mockOrderAccess.Setup(a => a.GetOrder(It.IsAny<Guid>())).Returns((Order)null);
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.Get(It.IsAny<Guid>());

            _mockOrderAccess.Verify(a => a.GetOrder(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetReturnsOkayWhenCustomerOrdersAreFound()
        {
            List<Order> orders = _fixture.CreateMany<Order>(10).ToList();
            Guid customerId = Guid.NewGuid();
            _mockOrderAccess.Reset();
            _mockOrderAccess.Setup(a => a.GetCustomerOrders(customerId)).Returns(orders);
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.GetCustomerOrders(customerId);

            _mockOrderAccess.Verify(a => a.GetCustomerOrders(customerId), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(orders.Count, ((IReadOnlyCollection<Order>)((OkObjectResult)result).Value).Count);
        }

        [Fact]
        public void GetReturns404WhenNoCustomerOrdersFound()
        {
            _mockOrderAccess.Reset();
            Guid customerId = Guid.NewGuid();
            _mockOrderAccess.Setup(a => a.GetCustomerOrders(customerId)).Returns(new List<Order>());
            OrdersController sut = CreateSystemUnderTest();

            var result = sut.GetCustomerOrders(customerId);

            _mockOrderAccess.Verify(a => a.GetCustomerOrders(customerId), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostReturnsOkayWhenCustomerExists()
        {
            Order order = _fixture.Create<Order>();
            _mockOrderAccess.Reset();
            Guid customerId = Guid.NewGuid();
            _mockOrderAccess.Setup(a => a.CreateOrderAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(order);
            OrdersController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(customerId);

            _mockOrderAccess.Verify(a => a.CreateOrderAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order.Id, ((Order)((OkObjectResult)result).Value).Id);
        }

        [Fact]
        public async Task PostReturns404WhenCustomerNotFound()
        {
            Order order = _fixture.Create<Order>();
            _mockOrderAccess.Reset();
            Guid customerId = Guid.NewGuid();
            _mockOrderAccess.Setup(a => a.CreateOrderAsync(customerId, It.IsAny<CancellationToken>())).ThrowsAsync(new CustomerNotFoundException(string.Empty));
            OrdersController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(customerId);

            _mockOrderAccess.Verify(a => a.CreateOrderAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteReturnsOkay()
        {
            Order order = _fixture.Create<Order>();
            _mockOrderAccess.Reset();
            Guid orderId = Guid.NewGuid();
            OrdersController sut = CreateSystemUnderTest();

            var result = await sut.DeleteAsync(orderId);

            _mockOrderAccess.Verify(a => a.ClearOrderAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<OkResult>(result);
        }


        private OrdersController CreateSystemUnderTest()
        {
            return new OrdersController(_mockOrderAccess.Object);
        }
    }
}
