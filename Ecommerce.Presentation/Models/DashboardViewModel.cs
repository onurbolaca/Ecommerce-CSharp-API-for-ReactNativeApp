using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Models
{
	public class DashboardViewModel
	{
		public DashboardViewModel()
		{
			Categories = new List<Category>();
			Products = new List<Product>();
		}

		public List<Category> Categories { get; set; }
		public List<Product> Products { get; set; }
	}
}