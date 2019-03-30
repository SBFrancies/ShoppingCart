using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Api.Filters;
using ShoppingCartApi.Common.Interface;

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
        public IActionResult Get([FromRoute]Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{orderId}/{productId}/{quantity?}")]
        public async Task<IActionResult> Post([FromRoute]Guid orderId, [FromRoute]Guid productId, [FromRoute]int quantity = 1, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}/{quantity}")]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromRoute]int quantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}