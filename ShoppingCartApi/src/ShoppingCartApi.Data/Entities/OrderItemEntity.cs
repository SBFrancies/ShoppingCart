using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Data.Entities
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public virtual OrderEntity Order { get; set; }

        public Guid ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        public int Quantity { get; set; }

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }
}
