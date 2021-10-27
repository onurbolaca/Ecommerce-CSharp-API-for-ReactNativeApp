using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.WebServis.Models
{
    public class ExpoNotificationModel
    {
        public string to { get; set; }
        public string sound { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string priority { get; set; }
    }
}