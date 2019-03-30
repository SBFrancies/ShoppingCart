using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Common.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
            
        }
    }
}
