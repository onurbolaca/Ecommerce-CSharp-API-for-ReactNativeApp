using Ecommerce.Entity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Helper
{
	public class CurrentSession
	{
		public static User OturumAcmisKullanici
		{
			get
			{
				return SessiondanVeriAl<User>("SessionaAttigimKullanici");
			}
		}

		public static Basket OturumAcmisKullanicininSepeti
		{
			get
			{
				return SessiondanVeriAl<Basket>("oturumAcmisAdaminSepeti");
			}
		}

		public static void SessionaVeriAt<T>(string key, T obj)
		{
			HttpContext.Current.Session[key] = obj;
		}

		//Product Olabilir
		//Kullanıcı Olabilir
		//int Olabilir
		//string Olabilir
		//double Olabilir
		//object Olabilir
		public static T SessiondanVeriAl<T>(string key)
		{
			//Öncelikli Olarak Gönderilen key ile session üzerine atama işlemi yapılmış mı ? Bunu Kontrol ediyorum
			if (HttpContext.Current.Session[key] != null)
				return (T)HttpContext.Current.Session[key];

			else
				return default(T);
		}

		public static void SessionTemizle()
		{
			HttpContext.Current.Session.Clear();
		}

		public static void SessionKeyTemizle(string key)
		{
			if (HttpContext.Current.Session[key] != null)
				HttpContext.Current.Session.Remove(key);

		}
	}
}