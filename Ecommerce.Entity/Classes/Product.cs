using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class Product : BaseObject
	{

		[DisplayName("Ürün Kategorisi")]
		[Required(ErrorMessage = "{0} Seçilmesi Zorunludur.")]
		public int CategoryID { get; set; }
		public Category Category { get; set; }


		[DisplayName("Ürün Adı")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string ProductName { get; set; }


		[DisplayName("Ürün Açıklaması")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(500, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string ProductDescription { get; set; }

		[DisplayName("Birim Fiyat")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public decimal UnitPrice { get; set; }

		[DisplayName("Stok Miktarı")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public int StockAmount { get; set; }

		[DisplayName("Minimum Stok Miktarı")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public int MinimumStockAmount { get; set; }

		[DisplayName("Vergi Oranı")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public int VatRate { get; set; }

		[DisplayName("Son Kullanma Tarihi")]
		public DateTime? ExpiredDate { get; set; }

		public List<ProductImage> ProductImages { get; set; }

		[NotMapped]
		public string[] Images { get; set; }

		public virtual IEnumerable<ProductComment> ProductComments { get; set; }

	}
}
