using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class ProductComment : BaseObject
	{
		public int ProductID { get; set; }
		public Product Product { get; set; }

		[DisplayName("Yorum")]
		[StringLength(400, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz")]
		public string Comment { get; set; }


	}
}
