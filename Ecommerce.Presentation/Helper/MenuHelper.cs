using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Helper
{
	public class MenuHelper
	{
		public static List<Category> GetCategories()
		{
			CategoryManager manager = new CategoryManager();

			return manager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList.ToList();
		}
	}
}