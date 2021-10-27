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
	public class CategoryManager : IDataAccess<Category>
	{
		public DataContext db = new DataContext();

		public ResultObject<Category> Delete(Category _object)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();
				
			db.Products.Any(t => t.CategoryID == 1 && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted);

			_object.ObjectStatus = Entity.Enums.ObjectStatus.Deleted;
			db.Entry(_object).State = System.Data.Entity.EntityState.Modified;

			var result = db.SaveChanges();

			if (result > 0)
			{
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Message = "Kategori Başarıyla Silindi";
				resultObject.Url = "Index";
				resultObject._Object = _object;
			}

			else
			{
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Message = "Kategori Silinirken Hata Oluştu";
				resultObject._Object = _object;
			}


			return resultObject;
		}


		public ResultObject<Category> HardDelete(Category _object)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();

			db.Entry(_object).State = EntityState.Deleted;

			//Toplu Silme Hard Delete
			db.Products.RemoveRange(_object.Products);

			foreach (var item in _object.Products)
			{
				item.ObjectStatus = Entity.Enums.ObjectStatus.Deleted;
				item.Status = Entity.Enums.Status.Passive;

				db.Entry(item).State = EntityState.Modified;
			}

			var result = db.SaveChanges();

			if (result > 0)
			{
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Message = "Kategori Başarıyla Silindi";
				resultObject.Url = "Index";
				resultObject._Object = _object;
			}

			else
			{
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Message = "Kategori Silinirken Hata Oluştu";
				resultObject._Object = _object;
			}


			return resultObject;
		}


		public ResultObject<Category> Find(Expression<Func<Category, bool>> where)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();

			//Burada Yaptığımız sorgu ise koşullu bir şekilde istediğimiz kategoriyi verdiğimiz koşulda çağırmamızı sağlıyor. Örnek Kullanımı CategoryController içinde Update Action Resultuna bakabilirsiniz.
			resultObject._Object = db.Categories.Include(t => t.Products).FirstOrDefault(where);


			//Yukarıdaki Sorguda ResultObject üzerinde Category Tipinde Olan _Object Property'e  db.Categories.FirstOrDefault(where) sorgusu sonucu dönen sonucu atatadık.
			//Daha Sonrasında bu sorgu sonucu dönene bir Category varmı diye bakıyoruz. Eğer Boş ise Kullanıcıyı Bilgilendiriyoruz ve Liste Sayfasına geri dönmesi için URL Yazıyoruz
			if (resultObject._Object == null)
			{
				resultObject.Message = "Kategori Bulunamadı";
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Warning;
				resultObject.Url = "Index";
			}

			else
			{
				//Bu Durumda Else Doldurmamıza gerek yok aslında ancak ne amaçla kullabiliriz için örnek olarak bırakıyorum update sayfasına girince anlayacaksınız.
				resultObject.Message = string.Format("{0} Adlı Kategoriyi Güncellemektesiniz", resultObject._Object.CategoryName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Url = string.Format("Update/{0}", resultObject._Object.ID);
			}
			return resultObject;
		}

		public ResultObject<Category> Insert(Category _object)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();

			if (db.Categories.Any(t => t.CategoryName == _object.CategoryName))
			{
				resultObject.Message = string.Format("{0} Adlı Kategori Daha Önce Tanımlanmış", _object.CategoryName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Warning;

			}

			else
			{
				_object.ObjectStatus = Entity.Enums.ObjectStatus.NonDeleted;
				_object.Status = Entity.Enums.Status.Active;


				db.Categories.Add(_object);

				var result = db.SaveChanges();

				if (result > 0)
				{
					resultObject.Message = string.Format("{0} Adlı Kategori Başarıyla Eklendi", _object.CategoryName);
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
					resultObject.Url = "Index";
					resultObject._Object = _object;
				}

				else
				{
					resultObject.Message = string.Format("{0} Adlı Kategori Başarıyla Eklenemedi", _object.CategoryName);
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
					resultObject.Url = string.Empty;
				}
			}

			return resultObject;
		}

		public ResultObject<Category> List()
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();
			resultObject._ObjectList = db.Categories.ToList();
			return resultObject;
		}

		public ResultObject<Category> List(Expression<Func<Category, bool>> where)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();
			resultObject._ObjectList = db.Categories.Include(t => t.Products).Where(where).ToList();
			return resultObject;
		}


		public ResultObject<Category> Update(Category _object)
		{
			ResultObject<Category> resultObject = new ResultObject<Category>();

			db.Entry(_object).State = System.Data.Entity.EntityState.Modified;

			var result = db.SaveChanges();

			if (result > 0)
			{
				resultObject.Message = string.Format("{0} Adlı Kategori Başarıyla Güncellendi", _object.CategoryName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				resultObject.Url = "Index";
				resultObject._Object = _object;
			}

			else
			{
				resultObject.Message = string.Format("{0} Adlı Kategori Başarıyla Güncellenirken Hata Oluştu", _object.CategoryName);
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
				resultObject.Url = string.Empty;
			}

			return resultObject;
		}
	}
}
