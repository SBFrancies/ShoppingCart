﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApi.Common.Models
{
    public class OrderItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
