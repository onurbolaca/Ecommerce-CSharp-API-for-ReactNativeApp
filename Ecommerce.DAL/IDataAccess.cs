using Ecommerce.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
	public interface IDataAccess<T>
	{
		ResultObject<T> Insert(T _object);
		ResultObject<T> Update(T _object);
		ResultObject<T> Delete(T _object);
		ResultObject<T> Find(Expression<Func<T, bool>> where);
		ResultObject<T> List();
		ResultObject<T> List(Expression<Func<T, bool>> where);

	}
}
