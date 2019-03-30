using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Data.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
    }
}
