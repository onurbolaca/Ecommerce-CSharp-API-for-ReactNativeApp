using Ecommerce.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Presentation.Filter
{
	public class Exc : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			//NLOG -- NLOGCONFIG
			var kullanici = string.Empty;

			if (CurrentSession.OturumAcmisKullanici != null)
				kullanici = CurrentSession.OturumAcmisKullanici.NameAndSurname;

			var mesajim = string.Format("Controller Adı : {0} Action Adı: {1} Hatayı Alan Kullanıcı : {2} Hatanın Alınan Zamanı : {3} Alınan Hata: {4}", filterContext.RouteData.Values["controller"], filterContext.RouteData.Values["action"], kullanici, DateTime.Now.ToString(), filterContext.Exception.Message);

			//ObjectReferansException - 1000
			//NullReferansException -1001
			//DbException - 1002


			filterContext.ExceptionHandled = true;
			filterContext.Result = new ViewResult()
			{
				ViewName = "Error",
				ViewData = new ViewDataDictionary(mesajim)
			};
		}
	}
}