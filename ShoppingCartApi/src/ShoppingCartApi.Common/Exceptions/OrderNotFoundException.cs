using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Common.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message) : base(message)
        {
            
        }
    }
}
