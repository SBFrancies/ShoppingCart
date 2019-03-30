using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Data.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new HashSet<OrderItemEntity>();
    }
}
