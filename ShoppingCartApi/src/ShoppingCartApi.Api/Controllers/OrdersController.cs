using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Api.Filters;
using ShoppingCartApi.Common.Exceptions;
using ShoppingCartApi.Common.Interface;
using ShoppingCartApi.Common.Models;

namespace ShoppingCartApi.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [GeneralExceptionFilter]
    [Route("v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAccess _orderAccess;

        public OrdersController(IOrderAccess orderAccess)
        {
            _orderAccess = orderAccess ?? throw new ArgumentNullException(nameof(orderAccess));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            IReadOnlyCollection<Order> orders = _orderAccess.GetAllOrders();

            if (!orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get([FromRoute]Guid id)
        {
            Order order = _orderAccess.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet]
        [Route("{customerId}/customer")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetCustomerOrders([FromRoute]Guid customerId)
        {
            IReadOnlyCollection<Order> orders = _orderAccess.GetCustomerOrders(customerId);

            if (!orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpPost]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromRoute]Guid customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                Order order = await _orderAccess.CreateOrderAsync(customerId, cancellationToken);

                return Ok(order);
            }

            catch (CustomerNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id, CancellationToken cancellationToken = default)
        {
            await _orderAccess.ClearOrderAsync(id, cancellationToken);

            return Ok();
        }
    }
}