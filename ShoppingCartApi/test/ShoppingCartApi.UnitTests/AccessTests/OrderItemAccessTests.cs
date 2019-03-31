using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using ShoppingCartApi.Access;
using ShoppingCartApi.Common.Exceptions;
using ShoppingCartApi.Common.Models;
using ShoppingCartApi.UnitTests.DbFixtures;
using Xunit;

namespace ShoppingCartApi.UnitTests.AccessTests
{
    public class OrderItemAccessTests : ShoppingCartFixtureContext, IClassFixture<ShoppingCartEfDatabaseFixture>
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void CanGetOrderItem()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            OrderItem orderItem = sut.GetOrderItem(Guid.Parse("BB774E9D-CB0A-41FE-99BD-97780CA49217"));

            Assert.NotNull(orderItem);
        }

        [Fact]
        public async Task CanCreateOrderItem()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            OrderItem orderItem = await sut.CreateOrderItemAsync(Guid.Parse("51B5CFF8-B2CE-4DE8-AB8A-16B6343E105A"),
                Guid.Parse("C31A24E6-54E7-4644-B62E-C587001ED18F"), 4);

            Assert.NotNull(orderItem);
        }

        [Fact]
        public async Task ThrowsErrorWhenCreatingOrderItemForNonOrder()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync <OrderNotFoundException>(async () => await sut.CreateOrderItemAsync(
                Guid.NewGuid(),
                Guid.Parse("C31A24E6-54E7-4644-B62E-C587001ED18F"), 4));
        }

        [Fact]
        public async Task ThrowsErrorWhenCreatingOrderItemForNonProduct()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await sut.CreateOrderItemAsync(
                Guid.Parse("51B5CFF8-B2CE-4DE8-AB8A-16B6343E105A"),
                Guid.NewGuid(), 4));
        }

        [Fact]
        public async Task CanUpdateOrderItem()
        {
            int quantity = _fixture.Create<int>();
            OrderItemAccess sut = CreateSystemUnderTest();

            OrderItem orderItem =
                await sut.UpdateOrderItemQuantityAsync(Guid.Parse("43E4BA9F-5D82-4414-9702-5CA3DEF48168"), quantity);

            Assert.Equal(quantity, orderItem.Quantity);
        }

        [Fact]
        public async Task ThrowsErrorWhenUpdatingNonOrderItem()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<OrderItemNotFoundException>(async () => await sut.UpdateOrderItemQuantityAsync(
                Guid.NewGuid(), It.IsAny<int>()));
        }

        [Fact]
        public async Task CanDeleteOrderItem()
        {
            int quantity = _fixture.Create<int>();
            OrderItemAccess sut = CreateSystemUnderTest();

            await sut.RemoveOrderItemAsync(Guid.Parse("43E4BA9F-5D82-4414-9702-5CA3DEF48168"));
        }

        [Fact]
        public async Task ThrowsErrorWhenDeletingNonOrderItem()
        {
            OrderItemAccess sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<OrderItemNotFoundException>(async () => await sut.RemoveOrderItemAsync(
                Guid.NewGuid()));
        }

        private OrderItemAccess CreateSystemUnderTest() => new OrderItemAccess(CreateContext);
    }
}
