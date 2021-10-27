using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Presentation.Models
{
	public class LoginModel
	{
		[DisplayName("Mail Adresi")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public string EmailAdress { get; set; }


		[DisplayName("Parola")]
		[Required(ErrorMessage = "{0} Boş Geçilemez")]
		public string Password { get; set; }
	}
}