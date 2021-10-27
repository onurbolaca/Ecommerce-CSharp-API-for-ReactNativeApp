using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using ECommerce.WebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECommerce.WebServis.Controllers
{
    public class UserController : ApiController
    {
        UserManager userManager = new UserManager();

        [HttpGet]
        public List<User> GetAllUser()
        {
            return userManager.db.Users.ToList();
        }

        [HttpPost]
        public User Login([FromBody] LoginModel model)
        {
            return userManager.db.Users.FirstOrDefault(t => t.EMail == model.EMail && t.Password == model.Password);
        }

        [HttpPost]
        public ResultObject<User> RegisterNewUser([FromBody] User model)
        {
            model.UserType = Ecommerce.Entity.Enums.UserType.Standart;

            if (ModelState.IsValid)
                return userManager.RegisterNewUser(model);

            else
                return new ResultObject<User>() { Message = "Tüm Alanları Eksiksiz Doldurunuz" };

        }

        [HttpPost]
        public bool UpdateUser([FromBody] UserUpdateModel model)
        {
            try
            {
                var user = userManager.db.Users.FirstOrDefault(t => t.ID == model.ID);

                if (user != null)
                {
                    user.UserNotificationToken = model.NotificationToken;
                    user.PasswordRepeat = user.Password;
                    userManager.db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    userManager.db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return false;
        }
    }
}
