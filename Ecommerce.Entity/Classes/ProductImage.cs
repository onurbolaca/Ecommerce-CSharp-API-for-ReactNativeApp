using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class ProductImage : BaseObject
	{
		public int ProductID { get; set; }
		public virtual Product Product { get; set; }

		public string ProductImageUrl { get; set; }
	}
}
