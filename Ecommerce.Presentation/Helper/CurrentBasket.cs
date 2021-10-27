using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Helper
{
	public class CurrentBasket
	{
		public static Basket GetBasket()
		{
			if (CurrentSession.OturumAcmisKullanici != null)
			{
				BasketManager manager = new BasketManager();

				var basket = manager.GetCurrentBasket(CurrentSession.OturumAcmisKullanici.ID);

				if (basket != null)
				{
					foreach (var item in basket.BasketItems)
						basket.TotalPrice += item.Product.UnitPrice * item.Quantity;
				}

				//Session Üzerine Sepet Atama
				//CurrentSession.SessionaVeriAt<Basket>("oturumAcmisAdaminSepeti", basket);

				return basket;
			}

			else
				return null;

		}
	}
}