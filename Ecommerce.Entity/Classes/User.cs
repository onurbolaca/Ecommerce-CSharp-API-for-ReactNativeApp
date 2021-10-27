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
	public class User : BaseObject
	{

		[DisplayName("Ad")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string Name { get; set; }


		[DisplayName("Soyad")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(2, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		public string Surname { get; set; }


		[DisplayName("Parola")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(4, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		[DataType(DataType.Password)]
		[Compare("PasswordRepeat", ErrorMessage = "Parolalar Eşleşmiyor")]
		public string Password { get; set; }


		[DisplayName("Parola Tekrar")]
		[NotMapped]
		[DataType(DataType.Password)]
		public string PasswordRepeat { get; set; }


		[DisplayName("E-Mail")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		[StringLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olamaz.")]
		[MinLength(4, ErrorMessage = "{0} {1} Karakterden Küçük Olamaz.")]
		[DataType(DataType.EmailAddress)]
		public string EMail { get; set; }


		[DisplayName("Kullanıcı Tipi")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public UserType UserType { get; set; }


		[DisplayName("Adres")]
		[StringLength(500, ErrorMessage = "{0} Karakterden Fazla Olamaz !")]
		public string Address { get; set; }


		[NotMapped]
		public string NameAndSurname
		{
			get
			{
				return string.Format("{0} {1}", Name, Surname);
			}
		}

		public string UserNotificationToken { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
