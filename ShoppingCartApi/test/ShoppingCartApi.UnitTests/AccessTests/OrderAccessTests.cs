using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartApi.Access;
using ShoppingCartApi.Common.Exceptions;
using ShoppingCartApi.Common.Models;
using ShoppingCartApi.UnitTests.DbFixtures;
using Xunit;

namespace ShoppingCartApi.UnitTests.AccessTests
{
    public class OrderAccessTests : ShoppingCartFixtureContext, IClassFixture<ShoppingCartEfDatabaseFixture>
    {
        [Fact]
        public void CanGetAllOrders()
        {
            OrderAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Order> orders = sut.GetAllOrders();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public void CanGetOrderById()
        {
            OrderAccess sut = CreateSystemUnderTest();

            Order order = sut.GetOrder(Guid.Parse("51B5CFF8-B2CE-4DE8-AB8A-16B6343E105A"));

            Assert.NotNull(order);
        }

        [Fact]
        public void CanGetCustomerOrders()
        {
            OrderAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Order> orders =
                sut.GetCustomerOrders(Guid.Parse("56DEF7D9-061C-4A26-BDF6-81695B074DD7"));

            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task CanCreateOrder()
        {
            OrderAccess sut = CreateSystemUnderTest();

            Order order = await sut.CreateOrderAsync(Guid.Parse("56DEF7D9-061C-4A26-BDF6-81695B074DD7"));

            Assert.NotNull(order);
        }

        [Fact]
        public async Task ThrowsErrorWhenCreatingOrderForNonCustomer()
        {
            OrderAccess sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<CustomerNotFoundException>(async () => await sut.CreateOrderAsync(Guid.NewGuid()));
        }


        [Fact]
        public async Task CanClearOrder()
        {
            OrderAccess sut = CreateSystemUnderTest();

            await sut.ClearOrderAsync(Guid.Parse("51B5CFF8-B2CE-4DE8-AB8A-16B6343E105A"));
        }

        private OrderAccess CreateSystemUnderTest() => new OrderAccess(CreateContext);
    }
}
