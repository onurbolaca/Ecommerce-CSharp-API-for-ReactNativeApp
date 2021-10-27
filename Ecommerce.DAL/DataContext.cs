using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity.Classes;

namespace Ecommerce.DAL
{
	public class DataContext : DbContext
	{
		public DataContext() : base(CreateConnectionString())
		{
			var confing = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
			this.Configuration.ProxyCreationEnabled = false;
			//Layz Loading Özelliğini Pasif Eder
			//this.Configuration.LazyLoadingEnabled = false;
		}
		//AnyDesk
		public static string CreateConnectionString()
		{
			return new SqlConnectionStringBuilder() { InitialCatalog = "ECommerce_HI", UserID = "sa", DataSource = @"API", Password = "orneksifre" }.ToString();
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductComment> ProductComments { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<Basket> Baskets { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }


		// Bu kısım Managerlerımız üzerinde db.SaveChanges() methodunu çağırdığımızda Çalışan yer Önce Benim Yazdığım Kodu Çalıştır daha sonra Sen ne yapıcaksan ya diyoruz Kodumuza sen ne yapıcaksan yap dediğimiz yerde gitsin Insert Attsın Delete Atsın Update Attsın gibi methodun en altında **** return base.SaveChanges();** bu kısım söylemek istediğim yer 
		public override int SaveChanges()
		{
			//Burada sana gelen savechangesleri yakala dedik
			this.ChangeTracker.DetectChanges();

			// Sana Gelenler arasında Insert yeni ekleme olanlar varsa onları benim için bir değişkene atarmısın dedik. 
			var addedObjects = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(t => t.Entity).ToArray();

			//Eğer ekleme işlemleri varsa bizim için bunlar içinde dönermisin dedik
			foreach (var item in addedObjects)
			{
				//Burada sana geleneler BaseObjecten türemiş Nesneler haberin Olsun dedik.
				if (item is BaseObject)
				{
					//Sonrasında gelen Objeleri Sen BaseObjecten Türedik Haberin Olsun dedik ve Üzerinde Bulunan Propertylere Erişmesini Sağladık.
					var _obj = item as BaseObject;
					_obj.ObjectStatus = Entity.Enums.ObjectStatus.NonDeleted;
					_obj.Status = Entity.Enums.Status.Active;
					//_obj.CreatedBy = "system";
					_obj.CreatedOn = DateTime.Now;
					//_obj.LastModifiedBy = "system";
					_obj.LastModifiedOn = DateTime.Now;

					//Gelen İşlemi Insert olduğu için gerekli atamaları yapık.
				}

			}

			// Sana Gelenler arasında Update olanlar varsa onları benim için bir değişkene atarmısın dedik. 
			var UpdatedObjects = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Modified).Select(t => t.Entity).ToArray();

			foreach (var item in UpdatedObjects)
			{
				//Yukarıda Yazdığımızın Aynı Mantığında  Kontrollerini sağladık ve Gereki Değerleri atadık.
				if (item is BaseObject)
				{

					var _obj = item as BaseObject;
					_obj.Status = Entity.Enums.Status.Active;
					_obj.LastModifiedBy = "system";
					_obj.LastModifiedOn = DateTime.Now;
				}
			}

			//Ve En sonunda hadi sen git güncelleme ekleme ne varsa bildiğin gibi yap dedik bizim senle işimiz kalmadı dedik ve base.SaveChanges(); gidip veritabanında gerekli İşlemi yaptı 
			return base.SaveChanges();
		}
	}
}
