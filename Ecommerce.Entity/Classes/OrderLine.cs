using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class OrderLine : BaseObject
	{
		[DisplayName("Sipariş")]
		public int OrderID { get; set; }
		public virtual Order Order { get; set; }

		[DisplayName("Ürün")]
		public int ProductID { get; set; }
		public virtual Product Product { get; set; }

		[DisplayName("Miktar")]
		public int Quantity { get; set; }

		[DisplayName("Tutar")]
		public decimal Amount { get; set; }

	}
}
