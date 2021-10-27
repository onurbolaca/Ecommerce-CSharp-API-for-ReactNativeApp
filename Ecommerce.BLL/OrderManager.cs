using Ecommerce.DAL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL
{
	public class OrderManager : IDataAccess<Order>
	{
		public DataContext db = new DataContext();

		public ResultObject<Order> Delete(Order _object)
		{
			throw new NotImplementedException();
		}

		public ResultObject<Order> Find(Expression<Func<Order, bool>> where)
		{
			throw new NotImplementedException();
		}

		public ResultObject<Order> Insert(Order _object)
		{
			throw new NotImplementedException();
		}

		public ResultObject<Order> List()
		{
			throw new NotImplementedException();
		}

		public ResultObject<Order> List(Expression<Func<Order, bool>> where)
		{
			ResultObject<Order> resultObject = new ResultObject<Order>();

			resultObject._ObjectList = db.Orders.Include(t => t.OrderLines).Where(where).ToList();

			return resultObject;
		}

		public ResultObject<Order> Update(Order _object)
		{
			throw new NotImplementedException();
		}
	}
}
