using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entity.Enums
{
	public enum OrderStatus
	{
		[Display(Name = "Beklemede")]
		Waiting = 1,
		[Display(Name = "Onaylandı")]
		Approved = 2,
		[Display(Name = "Tamamlandı")]
		Completed = 3,
		[Display(Name = "İptal Edildi")]
		Cancelled = 4
	}
}
