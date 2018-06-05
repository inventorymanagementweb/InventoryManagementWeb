using InventoryManagement.Commons;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InventoryManagement.Identity.Model;

namespace InventoryManagement.Identity.DbContext
{
    public class IdentityDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public IdentityDbContext() : base(Constants.EntityFrameworkConnectionString)
        {
        }

        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<User>().HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<User>().HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<Role>().HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId }).ToTable("UserRoles", "User");

            modelBuilder.Entity<UserLogin>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey, ul.UserId }).ToTable("UserLogins", "User");

            modelBuilder.Entity<UserClaim>().HasKey(uc => uc.Id).ToTable("UserClaims", "User");
        }
    }
}
