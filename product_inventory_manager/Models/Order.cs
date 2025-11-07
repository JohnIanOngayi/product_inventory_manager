using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product_inventory_manager.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
    }
}