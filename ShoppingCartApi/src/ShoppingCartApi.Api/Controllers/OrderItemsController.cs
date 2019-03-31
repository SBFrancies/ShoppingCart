using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    [Route("v1/orderitems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemAccess _orderItemAccess;

        public OrderItemsController(IOrderItemAccess orderItemAccess)
        {
            _orderItemAccess = orderItemAccess ?? throw new ArgumentNullException(nameof(orderItemAccess));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get([FromRoute]Guid id)
        {
            OrderItem orderItem = _orderItemAccess.GetOrderItem(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        [Route("{orderId}/{productId}/{quantity=1}")]
        [ProducesResponseType(typeof(OrderItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromRoute]Guid orderId, [FromRoute]Guid productId, [FromRoute]int quantity = 1, CancellationToken cancellationToken = default)
        {
            try
            {
                if (quantity < 1)
                {
                    return BadRequest();
                }

                OrderItem orderItem = await _orderItemAccess.CreateOrderItemAsync(orderId, productId, quantity, cancellationToken);
                return Ok(orderItem);
            }

            catch (Exception exception) when (exception is OrderNotFoundException || exception is ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id}/{quantity}")]
        [ProducesResponseType(typeof(OrderItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync([FromRoute]Guid id, [FromRoute]int quantity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (quantity < 1)
                {
                    return BadRequest();
                }

                OrderItem orderItem = await _orderItemAccess.UpdateOrderItemQuantityAsync(id, quantity, cancellationToken);

                return Ok(orderItem);
            }

            catch (OrderItemNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _orderItemAccess.RemoveOrderItemAsync(id, cancellationToken);

                return Ok();
            }

            catch (OrderItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}