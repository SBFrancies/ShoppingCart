using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OrderManagerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task CanGetOrder()
        {
            Guid orderId = Guid.NewGuid();
            Order order = _fixture.Create<Order>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderAsync(orderId, It.IsAny<CancellationToken>())).ReturnsAsync(order);

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Order result = await sut.GetOrderAsync(orderId);

            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.Items.Count, result.Items.Count);
        
            for (int i = 0 ; i < order.Items.Count; i++)
            {
                Assert.Equal(order.Items[i].Quantity, result.Items[i].Quantity);
                Assert.Equal(order.Items[i].Product?.Name, result.Items[i].Product?.Name);
                Assert.Equal(order.Items[i].Product?.Description, result.Items[i].Product?.Description);
            }
        }

        [Fact]
        public async Task CanCaptureErrorGettingOrder()
        {
            Guid orderId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetOrderAsync(orderId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrderAsync(orderId));

            Assert.Equal($"Error fetching order {orderId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanGetAllOrders()
        {
            List<Order> orders = _fixture.CreateMany<Order>(_fixture.Create<int>()).ToList();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetAllOrdersAsync(It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            IList<Order> results = await sut.GetOrdersAsync();


            Assert.Equal(orders.Count, results.Count);

            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                Order result = results[i];

                Assert.Equal(order.Id, result.Id);
                Assert.Equal(order.Items.Count, result.Items.Count);

                for (int j = 0; j < order.Items.Count; j++)
                {
                    Assert.Equal(order.Items[j].Quantity, result.Items[j].Quantity);
                    Assert.Equal(order.Items[j].Product?.Name, result.Items[j].Product?.Name);
                    Assert.Equal(order.Items[j].Product?.Description, result.Items[j].Product?.Description);
                }
            }
        }

        [Fact]
        public async Task CanCaptureErrorGettingAllOrders()
        {
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetAllOrdersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrdersAsync());

            Assert.Equal("Error fetching orders, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanGetCustomerOrders()
        {
            Guid customerId = Guid.NewGuid();
            List<Order> orders = _fixture.CreateMany<Order>(_fixture.Create<int>()).ToList();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetCustomerOrdersAsync(customerId, It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            IList<Order> results = await sut.GetOrdersAsync(customerId);


            Assert.Equal(orders.Count, results.Count);

            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                Order result = results[i];

                Assert.Equal(order.Id, result.Id);
                Assert.Equal(order.Items.Count, result.Items.Count);

                for (int j = 0; j < order.Items.Count; j++)
                {
                    Assert.Equal(order.Items[j].Quantity, result.Items[j].Quantity);
                    Assert.Equal(order.Items[j].Product?.Name, result.Items[j].Product?.Name);
                    Assert.Equal(order.Items[j].Product?.Description, result.Items[j].Product?.Description);
                }
            }
        }

        [Fact]
        public async Task CanCaptureErrorGettingCustomerOrders()
        {
            Guid customerId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.GetCustomerOrdersAsync(customerId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.GetOrdersAsync(customerId));

            Assert.Equal($"Error fetching orders for customer {customerId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanClearOrder()
        {
            Guid orderId = Guid.NewGuid();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.ClearOrderAsync(orderId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            bool result = await sut.ClearOrderAsync(orderId);

            Assert.True(result);
        }

        [Fact]
        public async Task CanCaptureErrorClearingOrder()
        {
            Guid orderId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.ClearOrderAsync(orderId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.ClearOrderAsync(orderId));

            Assert.Equal($"Error clearing order {orderId}, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        [Fact]
        public async Task CanCreateOrder()
        {
            Guid orderId = Guid.NewGuid();
            Order order = _fixture.Create<Order>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.CreateOrderAsync(orderId, It.IsAny<CancellationToken>())).ReturnsAsync(order);

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Order result = await sut.CreateOrderAsync(orderId);

            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.Items.Count, result.Items.Count);

            for (int i = 0; i < order.Items.Count; i++)
            {
                Assert.Equal(order.Items[i].Quantity, result.Items[i].Quantity);
                Assert.Equal(order.Items[i].Product?.Name, result.Items[i].Product?.Name);
                Assert.Equal(order.Items[i].Product?.Description, result.Items[i].Product?.Description);
            }
        }

        [Fact]
        public async Task CanCaptureErrorCreatingOrder()
        {
            Guid orderId = Guid.NewGuid();
            string exceptionMessage = _fixture.Create<string>();

            Mock<IShoppingCartApi> mockApi = new Mock<IShoppingCartApi>();
            mockApi.Setup(a => a.CreateOrderAsync(orderId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            OrderManager sut = CreateSystemUnderTest(mockApi.Object);

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await sut.CreateOrderAsync(orderId));

            Assert.Equal("Error creating order, see inner exception for details", exception.Message);
            Assert.Equal(exceptionMessage, exception.InnerException.Message);
        }

        private OrderManager CreateSystemUnderTest(IShoppingCartApi api)
        {
            return new OrderManager(api);
        }
    }
}
