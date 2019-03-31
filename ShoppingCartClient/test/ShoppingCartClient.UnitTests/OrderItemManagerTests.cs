using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using ShoppingCartClient.Client;
using ShoppingCartClient.Client.ApiClient;
using ShoppingCartClient.Client.ApiClient.Models;
using Xunit;

namespace ShoppingCartClient.UnitTests
{
    public class OrderItemManagerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task CanGetOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();
            OrderItem orderItem = _fixture.Create<OrderItem>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderItemAsync(orderItemId, It.IsAny<CancellationToken>())).ReturnsAsync(orderItem);

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            OrderItem result = await sut.GetOrderItemAsync(orderItemId);

            Assert.Equal(orderItem.Quantity, result.Quantity);
            Assert.Equal(orderItem.Product?.Name, result.Product?.Name);
            Assert.Equal(orderItem.Product?.Description, result.Product?.Description);
        }

        [Fact]
        public async Task CanCaptureErrorGettingOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrderItemAsync(orderItemId));

            Assert.Equal($"Error fetching order item {orderItemId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanDeleteOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.DeleteOrderItemAsync(orderItemId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            bool result = await sut.DeleteOrderItemAsync(orderItemId);

            Assert.True(result);
        }

        [Fact]
        public async Task CanCaptureErrorDeletingOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.DeleteOrderItemAsync(orderItemId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.DeleteOrderItemAsync(orderItemId));

            Assert.Equal($"Error deleting order item {orderItemId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanCreateOrderItem()
        {
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            OrderItem orderItem = _fixture.Build<OrderItem>().With(a => a.Quantity, quantity).Create();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>())).ReturnsAsync(orderItem);

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            OrderItem result = await sut.CreateOrderItemAsync(orderId, productId, quantity);

            Assert.Equal(orderItem.Quantity, result.Quantity);
            Assert.Equal(orderItem.Product?.Name, result.Product?.Name);
            Assert.Equal(orderItem.Product?.Description, result.Product?.Description);
        }

        [Fact]
        public async Task CanCaptureErrorCreatingOrderItem()
        {
            Guid orderId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.CreateOrderItemAsync(orderId, productId, quantity, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.CreateOrderItemAsync(orderId, productId, quantity));

            Assert.Equal("Error creating order item, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanUpdateOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            OrderItem orderItem = _fixture.Build<OrderItem>().With(a => a.Quantity, quantity).Create();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.UpdateOrderItemAsync(orderItemId, quantity, It.IsAny<CancellationToken>())).ReturnsAsync(orderItem);

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            OrderItem result = await sut.UpdateOrderItemQuantitytAsync(orderItemId, quantity);

            Assert.Equal(orderItem.Quantity, result.Quantity);
            Assert.Equal(orderItem.Product?.Name, result.Product?.Name);
            Assert.Equal(orderItem.Product?.Description, result.Product?.Description);
        }

        [Fact]
        public async Task CanCaptureErrorUpdatingOrderItem()
        {
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.UpdateOrderItemAsync(orderItemId, quantity, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.UpdateOrderItemQuantitytAsync(orderItemId, quantity));

            Assert.Equal($"Error updating order item {orderItemId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanIncrementOrderItemQuantity()
        {
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            OrderItem orderItem = _fixture.Build<OrderItem>().With(a => a.Quantity, quantity).Create();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderItemAsync(orderItemId, It.IsAny<CancellationToken>())).ReturnsAsync(orderItem);
            mockApi.Setup(a => a.UpdateOrderItemAsync(orderItemId, quantity +1 , It.IsAny<CancellationToken>())).ReturnsAsync(new OrderItem(orderItem.Product, orderItem.Quantity + 1));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            OrderItem result = await sut.IncrementOrderItemQuantitytAsync(orderItemId);

            Assert.Equal(orderItem.Quantity, result.Quantity);
        }

        [Fact]
        public async Task CanDecrementOrderItemQuantity()
        {
            Guid orderItemId = Guid.NewGuid();
            int quantity = _fixture.Create<int>();
            OrderItem orderItem = _fixture.Build<OrderItem>().With(a => a.Quantity, quantity).Create();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderItemAsync(orderItemId, It.IsAny<CancellationToken>())).ReturnsAsync(orderItem);
            mockApi.Setup(a => a.UpdateOrderItemAsync(orderItemId, quantity - 1, It.IsAny<CancellationToken>())).ReturnsAsync(new OrderItem(orderItem.Product, orderItem.Quantity - 1));

            OrderItemManager sut = CreateSystemUnderTest(mockApi.Object);

            OrderItem result = await sut.DecrementOrderItemQuantitytAsync(orderItemId);

            Assert.Equal(orderItem.Quantity, result.Quantity);
        }

        private OrderItemManager CreateSystemUnderTest(IShoppingCartApi api)
        {
            return new OrderItemManager(api);
        }
    }
}
