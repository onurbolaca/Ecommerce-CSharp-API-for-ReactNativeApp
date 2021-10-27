using Ecommerce.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Classes
{
	public class Order : BaseObject
	{
		[DisplayName("Sipariş Numarası")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		public string OrderNumber { get; set; }

		[DisplayName("Sipariş Tarihi")]
		public DateTime OrderDate { get; set; }

		[DisplayName("Siparişi Veren Kullanıcı")]
		public int UserID { get; set; }
		public virtual User User { get; set; }

		[DisplayName("Sipariş Durumu")]
		public OrderStatus OrderStatus { get; set; }


		[DisplayName("Toplam Tutar")]
		public decimal TotalAmount { get; set; }

		public virtual List<OrderLine> OrderLines { get; set; }
	}
}
