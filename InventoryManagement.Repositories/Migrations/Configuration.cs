using System;

namespace InventoryManagement.Repositories.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infastructure.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infastructure.DatabaseContext context)
        {
            context.Users.AddOrUpdate(
                new Models.Identity.User
                {
                    Id = Guid.Parse("69f1b727-ab23-47e7-b6aa-8b22095b6674"),
                    UserName = "nqthien041292",
                    Email = "nqthien041292@gmail.com",
                    PasswordHash = "AFxmuiOhQEfqHW9ZFyRWTOqySMOK4bZYWOfq8F9qP+Jk2kcxZiJv9fH8lZ4ZE1ZSsg==",
                    SecurityStamp = Guid.Parse("d5bf7530-145d-4a24-867e-0c7c654f559f").ToString("D"),
                    EmailConfirmed = true,
                    FullName = "Thien Nguyen"
                }
            );

            context.SaveChanges();
        }
    }
}
