using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Api.Filters;
using ShoppingCartApi.Common.Interface;

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
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute]Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{userId}/user")]
        public IActionResult GetUserOrders([FromRoute]Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{userId")]
        public async Task<IActionResult> Post([FromRoute]Guid userId, CancellationToken cancellationToken = default)
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