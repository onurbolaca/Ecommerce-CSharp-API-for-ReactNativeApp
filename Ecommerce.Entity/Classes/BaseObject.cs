using Ecommerce.Entity.Enums;
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
	public class BaseObject
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[DisplayName("Oluşturulma Tarihi")]
		[ScaffoldColumn(false)]
		public DateTime? CreatedOn { get; set; }

		[DisplayName("Güncellenme Tarihi")]
		[ScaffoldColumn(false)]
		public DateTime? LastModifiedOn { get; set; }

		[DisplayName("Oluşturan Kullanıcı")]
		[ScaffoldColumn(false)]
		public string CreatedBy { get; set; }

		[DisplayName("Güncelleyen Kullanıcı")]
		[ScaffoldColumn(false)]
		public string LastModifiedBy { get; set; }

		[DisplayName("Silindi Bilgisi")]
		[DefaultValue(1)]
		[ScaffoldColumn(false)]
		public ObjectStatus ObjectStatus { get; set; }

		[DisplayName("Durum")]
		[DefaultValue(1)]
		public Status Status { get; set; }
	}
}
