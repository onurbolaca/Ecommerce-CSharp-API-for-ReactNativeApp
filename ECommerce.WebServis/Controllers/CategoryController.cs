using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECommerce.WebServis.Controllers
{
	public class CategoryController : ApiController
	{
		public CategoryManager manager = new CategoryManager();
		public ProductManager productManager = new ProductManager();


		[HttpPost]
		public Category InsertNewCategory([FromBody] Category model)
		{
			if (ModelState.IsValid)
			{
				var result = manager.Insert(model)._Object;

				return result;
			}

			else
				return model;

		}

		[HttpPost]
		public ResultObject<Category> UpdateCategory([FromBody] Category model)
		{
			ResultObject<Category> categoryResultObject = new ResultObject<Category>();

			if (model.ID == 0)
				ModelState.AddModelError("ID", "ID Parametresini Doldurunuz");

			if (ModelState.IsValid)
			{
				var category = manager.Find(t => t.ID == model.ID)._Object;

				if (category != null)
				{
					category.CategoryName = model.CategoryName;
					category.CategoryDescription = model.CategoryDescription;

					categoryResultObject = manager.Update(category);

					return categoryResultObject;
				}

				else
				{
					categoryResultObject.Message = "Gönderdiğiniz Parametreyle Eşleşen Kategori Bulunamadı";

					return categoryResultObject;
				}

			}

			else
			{
				categoryResultObject.Message = "Lütfen CategoryName , CategoryDescription ve ID  Alanlarını Doldurunuz (ID = 0 Olamaz)";

				return categoryResultObject;
			}
		}

		[HttpPost]
		public ResultObject<Category> DeleteCategory([FromBody] Category model)
		{

			ResultObject<Category> categoryResultObject = new ResultObject<Category>();

			if (model.ID == 0)
			{
				categoryResultObject.Message = "ID alanını Doldurunuz";
				return categoryResultObject;
			}



			else
			{
				if (manager.db.Products.Any(t => t.CategoryID == model.ID))
				{
					categoryResultObject.Message = "Silmek İstediğiniz Kategoride Ürün Bağlı Olduğu İçin Silme İşlemine Devam Edilemiyor";
					return categoryResultObject;
				}


				else
				{
					var category = manager.Find(t => t.ID == model.ID)._Object;

					if (category == null)
					{
						categoryResultObject.Message = "Kategori Bulunamadı";

						return categoryResultObject;
					}

					else
						return manager.Delete(category);

				}
			}
		}

		[HttpGet]
		public ResultObject<Product> GetProductByCategoryID(int id)
		{

			return productManager.List(t => t.ObjectStatus == Ecommerce.Entity.Enums.ObjectStatus.NonDeleted && t.Status == Ecommerce.Entity.Enums.Status.Active && t.CategoryID == id);
		}

		[HttpGet]
		public ResultObject<Category> GetAllCategory()
		{
			return manager.List(t => t.ObjectStatus == Ecommerce.Entity.Enums.ObjectStatus.NonDeleted && t.Status == Ecommerce.Entity.Enums.Status.Active);
		}

		//JsonConvert.SerializeObject(positionCompareRatingDto, settings);

		public JsonSerializerSettings GetSettings()
		{
			return new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				Error = (sender, args) =>
				{
					args.ErrorContext.Handled = true;
				},
			};
		}
	}
}
