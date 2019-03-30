using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Common.Exceptions
{
    public class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException(string message) : base(message)
        {
            
        }
    }
}
