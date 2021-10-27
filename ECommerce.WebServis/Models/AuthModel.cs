using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ECommerce.WebServis.Models
{
	public class AuthModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }


		public static bool IsAuthorize(HttpRequestMessage request)
		{
			if (request.Headers.GetValues("UserName").FirstOrDefault() == "Kagan" && request.Headers.GetValues("Password").FirstOrDefault() == "123")
				return true;
			else
				return false;
		}
	}
}