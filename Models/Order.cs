using System;
using System.Collections.Generic;
using System.Text;

namespace WebCoreAppBasics.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal OrderTotal { get; set; } = 0;
        public int OrderStatusId { get; set; } = 1;
        public decimal TaxPrice { get; set; } = 0;
        public DateTime? SuccessDate { get; set; }
        public int NrItems { get; set; } = 0;
        public List<OrderProducts>? OrderList { get; set; }
    }

    public class OrderProducts
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductMealId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; } = "R";
        public decimal ProductPrice { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; } = 0;
    }
}
