using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class Category : BaseObject
	{
		[DisplayName("Kategori Adı")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string CategoryName { get; set; }


		[DisplayName("Kategori Açıklaması")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(500, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string CategoryDescription { get; set; }

		[ScaffoldColumn(false)]
		public string CategoryBannerUrl { get; set; }

		public List<Product> Products { get; set; }
	}
}
