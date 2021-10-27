using Ecommerce.DAL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL
{
	public class UserManager
	{
		public DataContext db = new DataContext();
		public ResultObject<User> Login(string EmailAdress, string Password)
		{
			ResultObject<User> resultObject = new ResultObject<User>();

			var user = db.Users.FirstOrDefault(t => t.EMail == EmailAdress && t.Password == Password && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted && t.Status == Entity.Enums.Status.Active);

			if (user != null)
			{
				resultObject.Message = "Giriş Başarılı";
				resultObject.Url = "Dashboard";
				resultObject._Object = user;
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
			}

			else
			{
				resultObject.Message = "Kullanıcı Bulunamadı";
				resultObject.Url = string.Empty;
				resultObject._Object = user;
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Warning;
			}

			return resultObject;
		}

		public bool IsMailAdressExist(string EMail)
		{
			return db.Users.Any(t => t.EMail == EMail);
		}

		public ResultObject<User> NewUser(User model)
		{
			ResultObject<User> resultObject = new ResultObject<User>();

			try
			{
				db.Users.Add(model);

				if (db.SaveChanges() > 0)
				{
					resultObject.Message = "Kayıt İşlemi Başarılı";
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				}

				else
				{
					resultObject.Message = "Teknik Hata oluştu";
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
				}
			}
			catch (Exception)
			{

				resultObject.Message = "Teknik Hata oluştu";
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
			}

			return resultObject;
		}

		public ResultObject<User> RegisterNewUser(User model)
		{
			ResultObject<User> resultObject = new ResultObject<User>();

			try
			{
				if (db.Users.Any(t => t.EMail == model.EMail))
				{
					resultObject.Message = "Mail Adresi Daha Önce Sisteme Kayıt Edilmiş";
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Warning;
					return resultObject;
				}

				db.Users.Add(model);
				var result = db.SaveChanges();

				if (result > 0)
				{
					resultObject._Object = model;
					resultObject.Message = "Başarılı Bir Şekilde Kayıt Oldunuz";
					resultObject.ResultStatus = Entity.Enums.ResultStatus.Success;
				}
			}
			catch (Exception)
			{
				resultObject.Message = "Teknik Bir Hata Oluştu";
				resultObject.ResultStatus = Entity.Enums.ResultStatus.Error;
			}

			return resultObject;
		}
	}
}
