using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using Ecommerce.Presentation.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Presentation.Controllers
{
	public class CategoryController : Controller
	{
		public CategoryManager manager = new CategoryManager();

		[Auth]
		public ActionResult Index()
		{
			return View(manager.List(t => t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._ObjectList);
		}

		//Insert Action Result Category Eklememiz için bize bir view oluşturur.
		public ActionResult Insert()
		{
			HttpContext.Session["girisYapmısKullanci"] = "Derya";

			return View();
		}

		public ActionResult Detay(string url)
		{
			var result = ToUrlSlug("kağan açc");

			return View();
		}


		public static string ToUrlSlug(string value)
		{

			//First to lower case 
			value = value.ToLowerInvariant();

			//Remove all accents
			var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

			value = Encoding.ASCII.GetString(bytes);

			//Replace spaces 
			value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

			//Remove invalid chars 
			value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

			//Trim dashes from end 
			value = value.Trim('-', '_');

			//Replace double occurences of - or \_ 
			value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

			return value;
		}

		//Burdaki Action Result ise View > Category > Insert View altında Kaydet butonuna basıldıktan sonra çalışıcak Action Resultumuzdur.
		[HttpPost]
		public ActionResult Insert(Category model)
		{
			//Model State Is Valid amacı : Category Insert View üzerinde modelimizin tipi Category olduğu için ECommerce.Entitiy.Classes içinde Category.cs üzerindeki DataAnotationların Doğruluklarını bizim için Kontrol eder.
			if (ModelState.IsValid)
			{
				//Burada bir ResultObject tanımladık bunun amacı Category Manager üzerindeki Insert Methodumuz bize işlem sonucu ResultObject<Category> tipinde bir nesne dönecektir.manager.Insert(model); Alt satırda Insert yazan yerde F12 Basarak yazdığımız Methodu inceleyebilirsiniz.
				ResultObject<Category> result = manager.Insert(model);

				//Insert Sonucunda işlemimiz Başarılı ise RedirectTo Action Kullanıyoruz RedirectToAction Sayfa Yönlnedirmesi için Kullanıyoruz."result.Url" ise result objecten doldurup dönüyoruz
				if (result.ResultStatus == Entity.Enums.ResultStatus.Success)
				{
					TempData["mesaj1"] = "Ben Kaydettim";
					return RedirectToAction(result.Url);
				}


				else
				{
					//Buradaki Else Kısmı ise İşlem sonucunda bir problem olduğunu ve problemi view üzerinde gösterebilmemiz için ViewBag üzerine atıyoruz.Böylece kullanıcların ne problem ile karşılaştıklarını gösteriyoruz.
					ViewBag.Result = result;
					return View();
				}
			}

			return View();
		}

		//Burdaki Action Result İse bize update sayfasına yönlendirecek , ActionResult Update(int id) int id dediğimiz yer güncellemek istediğimiz kategorinin ID sini göndereceğiz. ve Bu ID li Categoryi bize bul getir demek için kullanacağız
		public ActionResult Update(int id)
		{
			//Burada ise üst yorum satırıda dediğimiz işlemi yaptık ID ye göre kategori var ise getir dedik.
			var resultObject = manager.Find(t => t.ID == id);

			//Kullanıcıyı Bilgilendirmek istersek bilgi amaçlı Viewbag doldurduk.Bilgilendirmek zorundada değiliz opsiyonlu karar tercihler sizin :)
			ViewBag.Result = resultObject;

			//CategoryManager üzerinde Find Methoduna baktık Dönen bir Category varmı diye yok ise Index yani Listeleme sayfasına gönderdik
			if (resultObject._Object == null)
				return RedirectToAction(resultObject.Url);

			//Var ise View Update View açıyoruz Bu Viewin Modeli ise CategoryTipinde
			else
				return View(resultObject._Object);

		}

		//Burası ise Update Sayfasında Kaydet Butonuna basıldıktan sonraki Kısım. 
		//** ÖNEMLİ Yukarıdaki Update Methodunda id Gönderdik Burda Niye Model gibi bir soru aklınıza takılabilir bunun cevabı ise
		//** CEVAP Üst taraftaki Update Action Resultu Liste sayfasında güncelle buttona basınca paramtere olarak id gönderiyoruz gönderdiğimiz idli bir category varmı diye bakıp modeli dönüyoruz.(Views>Category>Index.cshtml İnceleyebilirsiniz) Aşağıdaki Update te ise dolduruduğumuz modeli Gönderiyoruz ve güncelleme işlemi yapıyoruz
		[HttpPost]
		public ActionResult Update(Category model)
		{
			//Modelimizi Doğrumu Kontrol ettik. Detaylı Açıklması Insert ActionResult altında mevcut tekrar bakabilirsiniz.
			if (ModelState.IsValid)
			{
				//Modelimizi Category Manager üzerinde ki Update Methoduna gönderdik ve sonucu result Diye bir değişkene atadık.
				var result = manager.Update(model);

				//Kullanıcıyı Bİlgilendirmek için ViewBag üzerine Sonucu Attık
				ViewBag.Result = result;

				//Eğer Update İşlemim Başarılı ise Liste Sayfasına Yönlendirmesini Yaptık
				if (result.ResultStatus == Entity.Enums.ResultStatus.Success)
					return RedirectToAction(result.Url);

				//Eğer Problem Yaşadınysa Normal bir şekilde View Döndük
				else
					return View();

			}

			//Burdaki return View Bırakmamızın Sebebi ise ModalState Doğru Değilse validationlara takılıyor ise bilgilendirme yapabilmek için
			return View();
		}

		public ActionResult Delete(int id)
		{
			//Burada ise üst yorum satırıda dediğimiz işlemi yaptık ID ye göre kategori var ise getir dedik.
			var resultObject = manager.Find(t => t.ID == id);

			//Böyle Bir Kategori varmı diye kontrol ediyoruz Eğer var ise Manager içindeki Delete Methodunu Çağırıyoruz
			if (resultObject._Object != null)
			{
				manager.Delete(resultObject._Object);

				//daha Sonrasında Listeleme Sayfasına geri Dönüyoruz
				return RedirectToAction(resultObject.Url);
			}

			else
			{
				//Böyle bir kategori bulunamadıysa sorgumuza yönelik o zaman Kullanıcıyı Bilgilendirebilriz. 
				return View();
			}
		}

		public JsonResult DeleteAjax(int id)
		{

			var category = manager.Find(t => t.ID == id)._Object;

			if (category != null)
			{
				var result = manager.Delete(category);

				return Json(result);
			}

			return Json(null);
		}

		public ActionResult CategoryList(int id)
		{
			return View(manager.Find(t => t.ID == id && t.ObjectStatus == Entity.Enums.ObjectStatus.NonDeleted)._Object);
		}

	}
}