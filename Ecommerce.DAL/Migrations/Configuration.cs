namespace Ecommerce.DAL.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Ecommerce.Entity.Classes;

	internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce.DAL.DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(Ecommerce.DAL.DataContext context)
		{
			if (!context.Users.Any())
			{
				User user = new User()
				{
					Address = "Datahouse",
					CreatedBy = "system",
					CreatedOn = DateTime.Now,
					EMail = "kaganazar@gmail.com",
					LastModifiedBy = "system",
					LastModifiedOn = DateTime.Now,
					Name = "Kağan",
					Surname = "AZAR",
					ObjectStatus = Entity.Enums.ObjectStatus.NonDeleted,
					Status = Entity.Enums.Status.Active,
					Password = "12345",
					PasswordRepeat = "12345",
					UserType = Entity.Enums.UserType.Admin
				};

				context.Users.Add(user);
				context.SaveChanges();

				context.Users.FirstOrDefault(t => t.UserType == Entity.Enums.UserType.Admin);
			}
		}
	}
}
