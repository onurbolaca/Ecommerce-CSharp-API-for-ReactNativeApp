using Ecommerce.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Result
{
	public class ResultObject<T>
	{
		public ResultStatus ResultStatus { get; set; }
		public string Message { get; set; }
		public T _Object { get; set; }
		public IEnumerable<T> _ObjectList { get; set; }
		public string Url { get; set; }

	}
}
