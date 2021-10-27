using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class BasketItem : BaseObject
	{
		public int BasketID { get; set; }
		public virtual Basket Basket { get; set; }

		public int ProductID { get; set; }
		public virtual Product Product { get; set; }

		public int Quantity { get; set; }
	}
}
