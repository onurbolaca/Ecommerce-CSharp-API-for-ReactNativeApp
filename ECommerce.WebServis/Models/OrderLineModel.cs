using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.WebServis.Models
{
    public class OrderLineModel
    {
        public int id { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public int userId { get; set; }
    }
}