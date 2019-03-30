using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Data.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }

        public Guid  UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderItemEntity> Items { get; set; } = new HashSet<OrderItemEntity>();
    }
}
