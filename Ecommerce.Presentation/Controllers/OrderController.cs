using Ecommerce.BLL;
using Ecommerce.DAL;
using Ecommerce.Entity.Classes;
using Ecommerce.Presentation.Filter;
using Ecommerce.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ecommerce.Presentation.Controllers
{
	public class OrderController : Controller
	{
		private BasketManager manager = new BasketManager();
		public ActionResult Index()
		{
			var girisYapmisKullanıcı = HttpContext.Session["girisYapmısKullanci"];
			var sonUrun = HttpContext.Session["sonUrun"];
			return View();
		}

		[Auth]
		public ActionResult OrderDetail()
		{
			return View(CurrentBasket.GetBasket());
		}

		public ActionResult NewOrder()
		{
			var basket = CurrentBasket.GetBasket();

			using (DataContext context = new DataContext())
			{
				Order order = new Order()
				{
					OrderDate = DateTime.Now,
					OrderNumber = "OR-00000001",
					OrderStatus = Entity.Enums.OrderStatus.Waiting,
					TotalAmount = basket.TotalPrice,
					UserID = CurrentSession.OturumAcmisKullanici.ID,
					OrderLines = new List<OrderLine>()
				};

				foreach (var item in basket.BasketItems)
				{
					OrderLine orderLine = new OrderLine()
					{
						Amount = item.Product.UnitPrice,
						Product = item.Product,
						Order = order,
						Quantity = item.Quantity,
					};

					order.OrderLines.Add(orderLine);
				}

				context.Orders.Add(order);
				var result = context.SaveChanges();

				if (result > 0)
				{
					manager.DeleteBasket(CurrentSession.OturumAcmisKullanici.ID);
				}

			}
			return RedirectToAction("/Home/Index");
		}

		public ActionResult OrderList()
		{
			using (DataContext context = new DataContext())
			{
				return View(context.Orders.Include(t => t.User).Where(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted).ToList());
			}
		}

		public ActionResult SiparisDetay(int id)
		{
			using (DataContext context = new DataContext())
			{
				var orderLines = context.Orders.Include(t => t.User).Include(t => t.OrderLines).FirstOrDefault(t => t.ID == id);

				return View(orderLines);
			}
		}
	}
}