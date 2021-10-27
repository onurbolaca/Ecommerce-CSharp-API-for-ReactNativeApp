using Ecommerce.DAL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ecommerce.BLL
{
	public class ProductManager : IDataAccess<Product>
	{
		public DataContext db = new DataContext();

		public ResultObject<Product> Delete(Product _object)
		{
			throw new NotImplementedException();
		}

		public ResultObject<Product> Find(Expression<Func<Product, bool>> where)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			var product = db.Products.Include(t => t.Category).Include(t=> t.ProductImages).FirstOrDefault(where);

			if (product != null)
			{
				resultObject._Object = product;
			}

			else
			{
				resultObject.Message = "Gönderdiğiniz Parametrede Ürün Bulunamadı";
				resultObject._Object = null;
			}

			return resultObject;

		}

		public ResultObject<Product> Insert(Product _object)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();
			try
			{

				if (db.Products.Any(t => t.CategoryID == _object.CategoryID && t.ProductName == _object.ProductName))
				{
					resultObject.Message = string.Format("{0} Adlı Ürün Seçmiş Olduğunuz Kategoriye Daha Önce Eklenmiştir", _object.ProductName);
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Warning;
					resultObject._Object = _object;
					resultObject.Url = "Insert";
				}

				else
				{
					db.Products.Add(_object);
					var result = db.SaveChanges();

					if (result > 0)
					{
						resultObject.Message = string.Format("{0} Adlı Ürün Başarıyla Kayıt Edildi", _object.ProductName);
						resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
						resultObject._Object = _object;
						resultObject.Url = "Index";
					}

					else
					{
						resultObject.Message = string.Format("{0} Adlı Ürün Eklenirken Hata Oluştu.", _object.ProductName);
						resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
						resultObject._Object = _object;
						resultObject.Url = "Insert";
					}
				}


			}
			catch (Exception ex)
			{
				resultObject.Message = string.Format("Bilinmeyen Hata Oluştu", _object.ProductName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
				resultObject._Object = _object;
				resultObject.Url = "Insert";
			}

			return resultObject;
		}

		public ResultObject<Product> List()
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			resultObject._ObjectList = db.Products.ToList();

			return resultObject;
		}

		public ResultObject<Product> List(Expression<Func<Product, bool>> where)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			resultObject._ObjectList = db.Products.Include(t=> t.ProductImages).Where(where).ToList();
			return resultObject;
		}


		public ResultObject<Product> Update(Product _object)
		{
			ResultObject<Product> resultObject = new ResultObject<Product>();

			try
			{

				var currentProduct = db.Products.FirstOrDefault(t => t.ProductName == _object.ProductName && t.CategoryID == _object.CategoryID);

				if (currentProduct == null)
				{

					db.Entry(_object).State = System.Data.Entity.EntityState.Modified;

					var result = db.SaveChanges();

					if (result > 0)
					{
						resultObject.Message = "Güncelleme Başarılı";
						resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
						resultObject.Url = "Index";
						resultObject._Object = _object;
					}
				}


				else
				{
					//Bu Ürün Mevcut
				}

			}
			catch (Exception)
			{

				resultObject.Message = string.Format("Bilinmeyen Hata Oluştu", _object.ProductName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
				resultObject._Object = _object;
				resultObject.Url = string.Format("Update/{0}", _object.ID);
			}

			return resultObject;
		}


		public ResultObject<Product> GetDeletedProduct()
		{
			return new ResultObject<Product>();
		}
	}
}
