using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{

	//Alpercim Bu Sepet Senin Tabirinle CARD
	public class Basket : BaseObject
	{
		public int UserID { get; set; }
		public User User { get; set; }

		public decimal VatPrice { get; set; }
		public decimal TotalPrice { get; set; }

		public List<BasketItem> BasketItems { get; set; }
	}
}
