using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Data.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }

        public Guid  CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderItemEntity> Items { get; set; } = new HashSet<OrderItemEntity>();
    }
}
