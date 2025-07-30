using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMigrateApp.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}