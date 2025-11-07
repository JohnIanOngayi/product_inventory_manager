using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product_inventory_manager.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public virtual string ProductName { get; set; } = string.Empty; // Fetch in page loader??
        public int ProductQuantity { get; set; } // Ordered Quantity

        public float? ProductRate { get; set; } = null;
        public float OrderValue { get; set; }
    }
}