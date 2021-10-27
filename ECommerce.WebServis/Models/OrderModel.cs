using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.WebServis.Models
{
	public class OrderModel
	{
		public int UserId { get; set; }
		public List<OrderLineModel> OrderLines { get; set; }
	}
}