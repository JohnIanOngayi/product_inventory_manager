using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product_inventory_manager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; } // In inventory
        public float Price { get; set; }
    }
}