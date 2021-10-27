using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using ECommerce.WebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECommerce.WebServis.Controllers
{
    public class OrderController : ApiController
    {
        public OrderManager manager = new OrderManager();

        [HttpGet]
        public ResultObject<Order> GetOrdersByUserID(int id)
        {
            return manager.List(t => t.UserID == id);
        }

		[HttpPost]
		public ResultObject<Order> NewOrder([FromBody] OrderModel model)
		{
			Order order = new Order()
			{
				OrderLines = new List<OrderLine>(),
				OrderDate = DateTime.Now,
				OrderStatus = Ecommerce.Entity.Enums.OrderStatus.Waiting,
				TotalAmount = model.OrderLines.Sum(t => t.price * t.qty),
				UserID = model.UserId,
				OrderNumber = "Mobil-1"
			};

			foreach (var item in model.OrderLines)
			{
				OrderLine orderLine = new OrderLine()
				{
					Amount = item.price,
					Quantity = item.qty,
					ProductID = item.id,
					Status = Ecommerce.Entity.Enums.Status.Active,
					Order = order
				};

				order.OrderLines.Add(orderLine);
			}

			manager.db.Orders.Add(order);
			manager.db.SaveChanges();

			return new ResultObject<Order>();
		}
	}
}
