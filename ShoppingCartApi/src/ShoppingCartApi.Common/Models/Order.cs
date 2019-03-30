using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Common.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<OrderItem> Items { get; set; }
    }
}
