using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Presentation.Helper;

namespace Ecommerce.Presentation.Filter
{
	public class Auth : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (CurrentSession.OturumAcmisKullanici == null)
				filterContext.Result = new RedirectResult("/Home/Login");

			else
			{
				if (CurrentSession.OturumAcmisKullanici.UserType == Entity.Enums.UserType.Standart)
					filterContext.Result = new RedirectResult("/Home/Index");
			}
		}
	}
}