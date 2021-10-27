using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using Ecommerce.Presentation.Helper;
using Ecommerce.Presentation.Models;

namespace Ecommerce.Presentation.Controllers
{
	public class ProductController : Controller
	{
		ProductManager manager = new ProductManager();

		CategoryManager categoryManager = new CategoryManager();

		public ActionResult Index()
		{
			return View(manager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList);
		}

		public ActionResult Insert()
		{
			ViewBag.Categories = new SelectList(categoryManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList, "ID", "CategoryName");


			return View();
		}

		[HttpPost]
		public ActionResult Insert(Product model)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			var files = Request.Files;


			ViewBag.Categories = new SelectList(categoryManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList, "ID", "CategoryName");

			if (ModelState.IsValid)
			{

				resultObject = manager.Insert(model);

				ViewBag.Result = resultObject;

				if (resultObject.ResultStatus == Entity.Enums.ResultStatus.Success)
					return RedirectToAction(resultObject.Url);

			}

			return View(model);
		}

		public JsonResult FileUpload()
		{
			string fileNameUp = string.Empty;
			try
			{
				foreach (string fileName in Request.Files)
				{
					HttpPostedFileBase file = Request.Files[fileName];

					var extention = Path.GetExtension(file.FileName);

					CurrentSession.SessionaVeriAt<int>("sayi", 1);

					if (file != null && file.ContentLength > 0)
					{
						var path = Path.Combine(Server.MapPath("~/Content/ProductImage"));
						string pathString = System.IO.Path.Combine(path.ToString());
						bool isExists = System.IO.Directory.Exists(pathString);
						if (!isExists) System.IO.Directory.CreateDirectory(pathString);
						var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
						file.SaveAs(uploadpath);
					}
				}
			}
			catch (Exception)
			{

				throw;
			}

			return Json(fileNameUp);
		}

		public ActionResult Update(int id)
		{
			ViewBag.Categories = new SelectList(categoryManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList, "ID", "CategoryName");

			var product = manager.Find(t => t.ID == id)._Object;

			if (product == null)
				return RedirectToAction("Index");


			return View(product);
		}

		[HttpPost]
		public ActionResult Update(Product model)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			ViewBag.Categories = new SelectList(categoryManager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList, "ID", "CategoryName");

			if (ModelState.IsValid)
			{

				var product = manager.Find(t => t.ID == model.ID);

				resultObject = manager.Update(model);

				if (resultObject.ResultStatus == Entity.Enums.ResultStatus.Success)
					return RedirectToAction(resultObject.Url);

			}

			return View(model);
		}


		public ActionResult ProductDetail(int id)
		{
			var product = manager.Find(t => t.ID == id)._Object;

			ProductViewModel model = new ProductViewModel()
			{
				Product = product,
				Products = manager.List(t => t.CategoryID == product.CategoryID)._ObjectList.ToList()
			};

			return View(model);
		}
	}
}