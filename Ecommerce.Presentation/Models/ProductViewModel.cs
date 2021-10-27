using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Models
{
	public class ProductViewModel
	{
		public ProductViewModel()
		{
			Products = new List<Product>();
			Product = new Product();
		}

		public Product Product { get; set; }
		public List<Product> Products { get; set; }
	}
}