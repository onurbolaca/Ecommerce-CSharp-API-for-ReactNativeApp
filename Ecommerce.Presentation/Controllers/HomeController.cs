using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.BLL;
using Ecommerce.Presentation.Models;
using Ecommerce.Presentation.Helper;
using Ecommerce.Entity.Classes;
using Ecommerce.Presentation.Filter;

namespace Ecommerce.Presentation.Controllers
{
	[Exc]
	public class HomeController : Controller
	{
		CategoryManager manager = new CategoryManager();
		UserManager UserManager = new UserManager();
		ProductManager productManager = new ProductManager();
		BasketManager basketManager = new BasketManager();


		public ActionResult Index()
		{
			DashboardViewModel model = new DashboardViewModel()
			{
				Categories = manager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList.ToList(),
				Products = productManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList.ToList()
			};

			var res = CurrentSession.SessiondanVeriAl<int>("sayi");

			return View(model);
		}

		[Auth]
		public ActionResult Dashboard()
		{
			return View();
		}
		//Session ile ilgili Farklı Örnekler
		public ActionResult SessionOrnekleri()
		{
			var girisYapmisKullanici = CurrentSession.SessiondanVeriAl<User>("SessionaAttigimKullanici");

			var urunler = productManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted && t.ProductName == "Pantalon" || t.ProductName == "Gömlek")._ObjectList.ToList();

			CurrentSession.SessionaVeriAt<List<Product>>("TumUrunler", urunler);

			HttpContext.Session["SessionaAttigimKullanici"] = null;
			HttpContext.Session["ErdincinAldıgıUrun"] = productManager.Find(t => t.ProductName == "Gömlek");
			HttpContext.Session["Pantolon"] = manager.Find(t => t.CategoryName == "Test");



			//CurrentSession.SetValueToSession<string>("TestStringi", "Kağan");
			//CurrentSession.SetValueToSession<int>("sayi", 3);
			//CurrentSession.SetValueToSession<int>("sayi", 3);

			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{

			if (ModelState.IsValid)
			{
				var result = UserManager.Login(model.EmailAdress, model.Password);

				if (result._Object == null)
				{
					ViewBag.Sonuc = result;

					return View();
				}

				else
				{
					CurrentSession.SessionaVeriAt<User>("SessionaAttigimKullanici", result._Object);

					return RedirectToAction(result.Url);
				}


			}

			return View();
		}


		public ActionResult Register()
		{

			return View();
		}

		[HttpPost]
		public ActionResult Register(User model)
		{
			model.UserType = Entity.Enums.UserType.Standart;

			if (ModelState.IsValid)
			{
				if (UserManager.IsMailAdressExist(model.EMail))
				{
					ViewBag.Uyari = "Yazmış Olduğunuz Mail Adresi Kullanımda";
					return View(model);
				}

				ViewBag.Result = UserManager.NewUser(model);

			}

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}


		public ActionResult Error()
		{
			return View();
		}

		public ActionResult JqueryTest()
		{
			return View();
		}

		public JsonResult AddBasketOrUpdateBasket(int id)
		{
			return Json(basketManager.UpdateBasket(CurrentSession.OturumAcmisKullanici.ID, id, 1));
		}

		public JsonResult RemoveItemFromBasket(int id)
		{
			return Json(basketManager.DeleteBasketItem(CurrentSession.OturumAcmisKullanici.ID, id));
		}
	}
}