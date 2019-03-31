using System;
using System.Collections.Generic;
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
    public class OrderItemsControllerTests
    {
        private readonly Mock<IOrderItemAccess> _mockOrderItemAccess = new Mock<IOrderItemAccess>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetOrderItemReturnsOkayWhenOrderIsFound()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess.Setup(a => a.GetOrderItem(It.IsAny<Guid>())).Returns(orderItem);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = sut.Get(It.IsAny<Guid>());

            _mockOrderItemAccess.Verify(a => a.GetOrderItem(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull((OrderItem)((OkObjectResult)result).Value);
        }

        [Fact]
        public void GetOrderItemReturns404WhenOrderNotFound()
        {
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess.Setup(a => a.GetOrderItem(It.IsAny<Guid>())).Returns((OrderItem)null);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = sut.Get(It.IsAny<Guid>());

            _mockOrderItemAccess.Verify(a => a.GetOrderItem(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostOrderItemReturnsOkayWhenOrderItemCreated()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderItem);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(orderId, productId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull((OrderItem)((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task PostOrderItemReturns400WhenQuantityLessThan1()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = 0;
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderItem);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(orderId, productId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PostOrderItemReturns404WhenOrderNotFound()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OrderNotFoundException(string.Empty));
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(orderId, productId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostOrderItemReturns404WhenProductNotFound()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ProductNotFoundException(string.Empty));
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PostAsync(orderId, productId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutOrderItemReturnsOkayWhenOrderItemUpdated()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderItem);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PutAsync(orderItemId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull((OrderItem)((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task PutOrderItemReturns400WhenQuantityLessThan1()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderItemId = Guid.NewGuid();
            int quantity = 0;
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderItem);
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PutAsync(orderItemId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutOrderItemReturns404WhenOrderItmNotFound()
        {
            OrderItem orderItem = _fixture.Create<OrderItem>();
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess
                .Setup(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OrderItemNotFoundException(string.Empty));
            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.PutAsync(orderItemId, quantity, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.UpdateOrderItemQuantityAsync(orderItemId, quantity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteOrderItemReturnsOkayWhenOrderItemDeleted()
        {
            Guid orderItemId = Guid.NewGuid();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess.Setup(a => a.RemoveOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.DeleteAsync(orderItemId, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.RemoveOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteOrderItemReturns404WhenOrderItmNotFound()
        {
            Guid orderItemId = Guid.NewGuid();
            _mockOrderItemAccess.Reset();
            _mockOrderItemAccess.Setup(a => a.RemoveOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OrderItemNotFoundException(string.Empty));

            OrderItemsController sut = CreateSystemUnderTest();

            var result = await sut.DeleteAsync(orderItemId, It.IsAny<CancellationToken>());

            _mockOrderItemAccess.Verify(a => a.RemoveOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        private OrderItemsController CreateSystemUnderTest()
        {
            return new OrderItemsController(_mockOrderItemAccess.Object);
        }
    }
}
