using Ecommerce.DAL;
using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ecommerce.BLL
{
	public class BasketManager
	{
		public DataContext context = new DataContext();

		//yeni Sepet Oluştur
		public Basket NewBasket(int UserID)
		{
			Basket basket = new Basket()
			{
				UserID = UserID
			};

			context.Baskets.Add(basket);
			context.SaveChanges();

			return basket;
		}

		//Sepeti Güncelle Yeni Bir Ürün Ekleme veya Var olan Ürünü Güncelleme
		public bool UpdateBasket(int UserID, int ProductID, int Quantity)
		{
			var result = true;

			try
			{
				var currentBasket = context.Baskets.Include(t => t.BasketItems).FirstOrDefault(t => t.UserID == UserID && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);

				if (currentBasket == null)
				{
					var newBasket = NewBasket(UserID);

					BasketItem item = new BasketItem()
					{
						BasketID = newBasket.ID,
						ProductID = ProductID,
						Quantity = Quantity
					};

					var product = context.Products.FirstOrDefault(t => t.ID == item.ProductID);

					currentBasket.TotalPrice = product.UnitPrice * item.Quantity;

					context.BasketItems.Add(item);
					context.Entry(currentBasket).State = EntityState.Modified;
					context.SaveChanges();

				}

				else
				{
					if (currentBasket.BasketItems.Any(t => t.ProductID == ProductID))
					{
						var currentItem = currentBasket.BasketItems.FirstOrDefault(t => t.ProductID == ProductID);
						currentItem.Quantity = currentItem.Quantity + Quantity;

						if (currentItem.Quantity == 0 || currentItem.Quantity < 0)
							context.Entry(currentItem).State = EntityState.Deleted;


						else
							context.Entry(currentItem).State = EntityState.Modified;


						context.SaveChanges();
					}

					else
					{
						BasketItem item = new BasketItem()
						{
							BasketID = currentBasket.ID,
							ProductID = ProductID,
							Quantity = Quantity
						};

						context.BasketItems.Add(item);
						context.SaveChanges();
					}

					var UpdatecurrentBasket = context.Baskets.Include(t => t.BasketItems).FirstOrDefault(t => t.UserID == UserID && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);

					UpdatecurrentBasket.TotalPrice = 0;

					foreach (var item in UpdatecurrentBasket.BasketItems)
						UpdatecurrentBasket.TotalPrice += item.Quantity * item.Product.UnitPrice;


					context.Entry(UpdatecurrentBasket).State = EntityState.Modified;
					context.SaveChanges();
				}


			}
			catch (Exception)
			{

				result = false;
			}


			return result;

		}

		//Sepetten Ürün Silme
		public bool DeleteBasketItem(int UserID, int ProductID)
		{
			var result = true;

			try
			{
				var currentBasket = context.Baskets.Include(t => t.BasketItems).FirstOrDefault(t => t.UserID == UserID && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);

				if (currentBasket != null)
				{
					var currentItem = currentBasket.BasketItems.FirstOrDefault(t => t.ProductID == ProductID);

					if (currentItem != null)
					{
						context.Entry(currentItem).State = EntityState.Deleted;
						context.SaveChanges();
					}
				}
			}
			catch (Exception)
			{

				result = false;
			}

			return result;
		}

		//Sepeti Sil Ödeme Yaptığı Taktirde.
		public bool DeleteBasket(int UserID)
		{
			var result = true;

			try
			{
				var currentBasket = context.Baskets.Include(t => t.BasketItems).FirstOrDefault(t => t.UserID == UserID && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);

				if (currentBasket != null)
				{
					currentBasket.ObjectStatus = Entity.Enums.ObjectStatus.Deleted;

					context.Entry(currentBasket).State = EntityState.Modified;

					foreach (var item in currentBasket.BasketItems)
					{
						item.ObjectStatus = Entity.Enums.ObjectStatus.Deleted;

						context.Entry(item).State = EntityState.Modified;
					}

					context.SaveChanges();
				}
			}
			catch (Exception)
			{

				result = false;
			}

			return result;
		}

		//Mevcut Olan Sepeti Getirir
		public Basket GetCurrentBasket(int UserID)
		{
			return context.Baskets.Include(t => t.BasketItems).FirstOrDefault(t => t.UserID == UserID && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);
		}
	}
}
